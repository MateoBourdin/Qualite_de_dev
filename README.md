# TPRevision : API Produits et client Blazor

**Auteur :** Matéo Bourdin, BUT Informatique 3e année, IUT d'Annecy
**Dépôt GitHub :** https://github.com/MateoBourdin/Qualite_de_dev

Ce rendu regroupe dans une seule solution les TD sur les DTOs / AutoMapper et le TD sur le client Blazor avec le MVVM Toolkit.

| Projet | Rôle |
|---|---|
| `TPRevision` | API REST ASP.NET Core 8, EF Core, PostgreSQL |
| `TPRevisionTests` | Tests MSTest : contrôleurs (Moq), mapping AutoMapper, intégration sur PostgreSQL |
| `ClientBlazor` | Application Blazor Server (MVVM) qui consomme l'API |

---

## Objectif du projet

J'ai voulu faire un projet le plus fidèle possible aux principes SOLID, quitte à le rendre très abstrait. L'objectif n'est pas seulement de répondre au TD sur trois entités (Produit, Marque, TypeProduit), mais d'avoir une base réutilisable pour tous les autres projets du même type (API CRUD + DTOs + client). Ajouter une entité doit demander quelques lignes, pas de recopier un contrôleur ou un repository entier.

---

## Choix d'architecture

### Des interfaces petites et composables (ISP)

Plutôt qu'une grosse interface de repository, j'ai découpé chaque capacité dans sa propre interface :

| Interface | Responsabilité |
|---|---|
| `IReadable<T>` | `GetAllAsync`, `GetByIdAsync` |
| `IWritable<T>` | `AddAsync`, `UpdateAsync`, `DeleteAsync`, `SaveAsync` |
| `IDataRepository<T>` | Composition `IReadable<T>` + `IWritable<T>` (le CRUD) |
| `IDetailReadable<T>` | Lectures avec navigation (`Include`) |
| `ISearchable<T>` | Recherche par nom |
| `IQueryService<TListDto, TDetailDto>` | Lectures en DTO (affichage) |

Une entité ne dépend que de ce dont elle a besoin. Par exemple, `IMarqueRepository` est seulement une composition :

```csharp
public interface IMarqueRepository : IDataRepository<Marque>, IDetailReadable<Marque>, ISearchable<Marque> { }
```

J'ai d'abord eu une version avec une interface par entité qui contenait tout. Elle respectait mal l'ISP (un consommateur qui ne lit que des DTOs dépendait aussi des écritures), et les interfaces se ressemblaient trop. J'ai donc généralisé.

### Séparer persistance et lecture d'affichage (SRP)

- **Écritures** (POST, PUT, PATCH, DELETE) : elles se font avec l'entité complète, via `IDataRepository<T>`.
- **Affichage** : il passe par des DTOs, via `IQueryService<TListDto, TDetailDto>`. Les DTOs suppriment les références circulaires, cachent les propriétés internes, allègent les réponses et aplatissent les objets (`Type` et `Marque` sont des noms, pas des objets).

`IQueryService` prend deux types parce que la liste et le détail peuvent différer : `ProduitDto` (léger) / `ProduitDetailDto` (avec stock et `EnReappro`). Pour Marque et TypeProduit, le même DTO sert aux deux.

### AutoMapper avec `ProjectTo`

Les DTOs sont construits avec `ProjectTo<TDto>()`. La projection est traduite en SQL : seules les colonnes utiles sont lues, et `NbProduits` devient un `COUNT` côté base. AutoMapper reste en **8.1.1** pour rester compatible avec le reste du projet.

### Un contrôleur générique (`CrudController`)

Les trois contrôleurs étaient presque identiques. J'ai écrit une classe abstraite générique :

```csharp
public abstract class CrudController<TEntity, TListDto, TDetailDto>(
    IDataRepository<TEntity> repo,
    IDetailReadable<TEntity> detailReader,
    IQueryService<TListDto, TDetailDto> queryService) : ControllerBase
```

Elle contient toutes les routes (GetAll, GetById, GetAllWithDetails, GetByIdWithDetails, POST, PUT, PATCH, DELETE). Un contrôleur concret se réduit à sa route et à la lecture de son identifiant :

```csharp
[Route("api/marque")]
public class MarqueController(IMarqueRepository repo, IQueryService<MarqueDto, MarqueDto> queryService)
    : CrudController<Marque, MarqueDto, MarqueDto>(repo, repo, queryService)
{
    protected override int GetId(Marque entity) => entity.IdMarque;
}
```

- **OCP** : on ajoute une entité sans modifier le contrôleur de base.
- **DIP** : les contrôleurs ne dépendent que d'interfaces, injectées par le conteneur de dépendances.
- La base gère aussi les erreurs : **404** pour une modification sur un id inexistant, **409** quand la base refuse une suppression (marque encore utilisée par des produits).

### Client Blazor en MVVM générique

Le client suit le pattern MVVM du TD (`CommunityToolkit.Mvvm` : `ObservableObject`, `[ObservableProperty]`, `IsLoading`, `LoadDataAsync`), avec la même logique de généralisation que l'API :

- `ApiService<TListDto, TDetailDto>` contient les appels HTTP, et chaque service ne donne que sa route (`MarqueService`, `ProduitService`, `TypeProduitService`).
- `ListViewModel<,>` et `DetailViewModel<,>` sont génériques et enregistrés une seule fois : `AddScoped(typeof(ListViewModel<,>))`.
- Les pages Razor ne font qu'afficher. Elles injectent le ViewModel et appellent `LoadDataAsync()`.
- Le client a ses propres DTOs, ce qui le découple des entités de l'API.

En Blazor Server, c'est le serveur du client qui appelle l'API, pas le navigateur. Il n'y a donc pas besoin de configurer CORS.

---

## Ajouter une nouvelle entité

Pour illustrer l'objectif de réutilisation, voici ce qu'il faut ajouter :

1. L'entité, le DTO et son `CreateMap` dans `MappingProfile`.
2. Une interface vide qui compose les capacités voulues (`IXxxRepository : IDataRepository<Xxx>, ...`) et son implémentation.
3. Un contrôleur de quelques lignes qui hérite de `CrudController`.
4. L'enregistrement dans `Program.cs`.
5. Côté client : le DTO, un service de 3 lignes (la route) et une page Razor. Les ViewModels sont déjà génériques.

---

## Corrections notables

- **Navigation EF** : `.WithMany()` sans argument créait des clés étrangères fantômes, et les listes `produits` restaient vides. J'ai corrigé avec `.WithMany(p => p.produits)`.
- **Migration `SuppressionColonnesFantomes`** : elle supprime les colonnes en trop (`idMarque1`, `MarqueIdMarque`…) créées par l'ancien modèle et des `[ForeignKey]` mal nommés. Elles empêchaient l'ajout de produits.
- **`EnReappro`** : en SQL, comparer avec `NULL` renvoie `NULL` et non `false`, ce qui provoquait une erreur 500 sur les produits sans stock. Les valeurs nulles sont maintenant testées explicitement.
- **Navigations nullables** : sans ça, `[ApiController]` exigeait `MarqueNavigation` et `TypeProduitNavigation` dans le POST d'un produit.

---

## Tests

21 tests MSTest :
- **contrôleurs** : tests unitaires avec Moq (repository et query service simulés) ;
- **profil AutoMapper** : configuration valide, `NbProduits`, `EnReappro` (y compris avec stock nul) ;
- **intégration** : sur une vraie base PostgreSQL (`ProduitsTestDB`, recréée à chaque test). Création puis relecture d'un produit, et cas d'un produit sans stock.

```powershell
dotnet test                                            # tout (PostgreSQL local requis)
dotnet test --filter "FullyQualifiedName!~Integration" # sans base de données
```

---

## Lancer le projet

Prérequis : .NET 8 SDK et PostgreSQL (chaîne de connexion dans `TPRevision/appsettings.json`).

```powershell
dotnet tool install --global dotnet-ef --version 8.0.11
dotnet ef database update --project TPRevision
psql -U postgres -d Produits -f seed_produits.sql      # données de test

dotnet run --project TPRevision --launch-profile https  # API + Swagger : https://localhost:7284/swagger
dotnet run --project ClientBlazor --launch-profile https # Client : https://localhost:7208
```

L'adresse de l'API utilisée par le client se règle dans `ClientBlazor/appsettings.json` (`ApiBaseUrl`).

---

## Limites et pistes d'amélioration

- **Sécurité** : l'API n'a pas d'authentification. Les entités sont exposées directement en écriture, il faudrait des DTOs d'entrée validés.
- **AutoMapper 8.1.1** est signalé avec une vulnérabilité connue (NU1903). Je l'ai gardé pour rester sur la version imposée par le projet.
- **Nommage** : certains `ActionName` hérités du premier contrôleur (`AddProduct`, `ModifyProduct`) sont génériques à tort. Les tests mériteraient une convention de nommage uniforme et une structure Arrange / Act / Assert plus systématique.

---

## Outils utilisés

Visual Studio 2022 et VS Code, PostgreSQL / pgAdmin, Swagger. Je me suis aidé d'un assistant IA (Claude) pour l'analyse des bugs, la vérification des tests et une partie du code. Les choix d'architecture et leur justification sont les miens.