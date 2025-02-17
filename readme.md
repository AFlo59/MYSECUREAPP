# MySecureApp

MySecureApp est une application web sécurisée développée en ASP.NET Core. Elle utilise Entity Framework Core pour la gestion de la base de données et ASP.NET Core Identity pour l'authentification et la gestion des utilisateurs.

## 📌 Prérequis

Avant de commencer, assurez-vous d'avoir installé sur votre machine :

- **.NET SDK 8.0**  
  Téléchargez-le sur [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download/dotnet/8.0).

- **SQL Server** (ou tout autre SGBD compatible avec EF Core)  
  Configurez votre instance ou utilisez une instance distante.

- **Git**  
  Pour cloner le dépôt.

- **Un éditeur de code**  
  Visual Studio Code, Visual Studio ou tout autre éditeur de votre choix.

## 🚀 Installation et Configuration

### 1️⃣ Cloner le projet

Ouvrez un terminal et exécutez les commandes suivantes :

```bash
git clone https://github.com/votre-utilisateur/MySecureApp.git
cd MySecureApp
```

### 2️⃣ Créer et configurer le fichier .env

Le projet utilise un fichier .env pour stocker la chaîne de connexion et d'autres variables d'environnement. À la racine du projet, créez un fichier nommé .env avec le contenu suivant :

```dotenv

SQLSERVER_HOST=myserver.database.windows.net
SQLSERVER_PORT=1433
SQLSERVER_USER=YOUR_USER
SQLSERVER_PASSWORD=YOUR_PASSWORD
SQLSERVER_DB=DATABASE_NAME

MODE_CONNECTION=DEV  # Choisissez DEV ou PROD selon votre environnement
MODE_HTTP=HTTP       # Choisissez HTTP ou HTTPS
PORT_HTTP=5050
PORT_HTTPS=5051
```
Adaptez ces valeurs à votre configuration locale (par exemple, pour une instance locale, vous pouvez utiliser localhost et les identifiants correspondants).

### 3️⃣ Restaurer les dépendances

Dans le terminal, exécutez :

```bash
 dotnet restore
```
Cette commande télécharge tous les packages NuGet nécessaires au projet.

### 4️⃣ Appliquer les migrations à la base de données

Le projet utilise Entity Framework Core pour la gestion des migrations et de la base de données.
Pour mettre à jour votre base de données avec la migration initiale, exécutez :

```bash
 dotnet ef database update
```
#### Note :
Si vous obtenez l'erreur The model for context 'ApplicationDbContext' has pending changes., cela signifie que vous avez modifié votre modèle sans créer de nouvelle migration. Dans ce cas, générez une nouvelle migration avec :

```bash
dotnet ef migrations add UpdateModels
dotnet ef database update
```

Si vous souhaitez repartir de zéro et recréer la migration initiale, vous pouvez supprimer la migration existante (attention, cela nécessite de réinitialiser la base si elle a déjà été mise à jour) 

```bash
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5️⃣ Lancer le projet

Compilez et lancez l'application en exécutant :

```bash
 dotnet run
```
Les messages de la console indiqueront que l'application démarre et vous donneront les adresses d'écoute. Par défaut, l'application est accessible aux URL suivantes :

HTTP : http://localhost:5000

HTTPS : https://localhost:5001

#### Astuce :
Vous verrez aussi un message indiquant que l'application démarre en mode HTTP sur le port configuré dans le fichier .env (ici, 5050). Assurez-vous que vos paramètres correspondent à vos besoins.

## 🛠 Problèmes et solutions

#### Erreur : Vue partielle '_Scripts' introuvable
L'erreur indique que l'application tente de rendre une vue partielle nommée _Scripts.cshtml mais ne la trouve pas. Pour corriger ce problème, vous pouvez :

##### Option 1 : Créer la vue partielle
Créez un fichier nommé _Scripts.cshtml dans le dossier Pages/Shared (ou Views/Shared) avec le contenu requis (même vide si vous ne souhaitez pas inclure de scripts communs).

##### Option 2 : Retirer l'appel à la vue partielle
Ouvrez votre fichier de layout (probablement Pages/Shared/_Layout.cshtml) et supprimez ou commentez la ligne :

```r
@await Html.PartialAsync("_Scripts")
```

#### Erreur PendingModelChangesWarning
Si vous voyez ce message lors de l'exécution de dotnet ef database update :

The model for context 'ApplicationDbContext' has pending changes.

Cela signifie que votre modèle a été modifié sans générer de nouvelle migration. Pour résoudre ce problème, générez une nouvelle migration :

```bash
dotnet ef migrations add UpdateModels
dotnet ef database update
```
#### Erreur : Migration 'InitialCreate' est déjà utilisée
Si vous obtenez l'erreur indiquant que le nom de migration InitialCreate est déjà utilisé, supprimez la migration existante puis recréez-la :

```bash
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 📜 Structure du projet

```bash
MySecureApp/
│
├── Controllers/          # Contrôleurs (si vous utilisez le pattern MVC)
├── Data/                 # Contexte de la base de données (ApplicationDbContext.cs)
├── Models/               # Modèles de données (UserEntity, Role, UserPublic, UserMetier, BaseEntity)
├── Pages/                # Pages Razor et vues partagées (_Layout.cshtml, etc.)
├── wwwroot/              # Fichiers statiques (CSS, JS, images)
├── appsettings.json      # Configuration de l'application
├── .env                  # Variables d'environnement (chaine de connexion, ports, etc.)
├── Program.cs            # Point d'entrée et configuration de l'application
└── README.md             # Documentation du projet (ce fichier)
```

## ✨ Contribuer

Les contributions sont les bienvenues !
Pour contribuer :

- Ouvrez une issue pour discuter de vos idées ou problèmes.
- Soumettez une pull request avec vos améliorations.

## 📄 Licence

Ce projet est sous licence MIT.
Voir le fichier LICENSE pour plus d’informations.

## 📢 Contact & Support

Pour toute question ou aide supplémentaire, vous pouvez ouvrir une issue sur GitHub ou me contacter directement via GitHub Issues. ! 🚀

