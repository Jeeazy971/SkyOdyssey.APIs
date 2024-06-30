# SkyOdyssey API

SkyOdyssey est une API conçue pour gérer les réservations de vols et d'hôtels, permettant aux utilisateurs de rechercher, réserver et payer pour des voyages de manière efficace. Cette API prend en charge l'intégration avec Stripe pour le traitement des paiements.

## Fonctionnalités

### Gestion des Utilisateurs
- **Création de compte utilisateur**
- **Connexion utilisateur**
- **Gestion des informations de l'utilisateur**

### Gestion des Locations
- **Création de location avec image**
- **Recherche de locations par ville, date de disponibilité, prix, et nombre maximum d'invités**
- **Récupération de toutes les locations**
- **Récupération d'une location par ID**

### Gestion des Vols
- **Récupération de tous les vols**
- **Récupération d'un vol par ID**

### Gestion des Réservations
- **Création de réservation**
- **Récupération de toutes les réservations**
- **Récupération d'une réservation par ID**
- **Mise à jour d'une réservation**
- **Suppression d'une réservation**

### Paiements
- **Traitement des paiements via Stripe**

## Prérequis

- .NET 8 SDK
- SQLite
- Docker
- Visual Studio Code
- Node.js (pour Angular/Ionic)

## Installation et Configuration

### Cloner le projet

```bash
git clone https://github.com/votre-repo/SkyOdyssey.git
cd SkyOdyssey
```

### Configuration de l'API

Créez un fichier `appsettings.json` dans le répertoire du projet avec le contenu suivant :

```json
{
  "Stripe": {
    "SecretKey": "votre_stripe_secret_key",
    "PublishableKey": "votre_stripe_publishable_key"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=skyodyssey.db"
  },
  "Jwt": {
    "Key": "votre_jwt_secret_key",
    "Issuer": "votre_issuer",
    "Audience": "votre_audience",
    "ExpiryMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Dockerfile

Créez un fichier `Dockerfile` à la racine du projet avec le contenu suivant :

```dockerfile
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SkyOdyssey/SkyOdyssey.csproj", "SkyOdyssey/"]
RUN dotnet restore "./SkyOdyssey/SkyOdyssey.csproj"
COPY . .
WORKDIR "/src/SkyOdyssey"
RUN dotnet build "./SkyOdyssey.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SkyOdyssey.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkyOdyssey.dll"]
```

### Exécuter le projet avec Docker

```bash
docker build -t skyodyssey-api .
```

### Exécuter le projet localement

Ouvrez le projet dans Visual Studio Code, puis exécutez les commandes suivantes dans le terminal intégré :

```bash
dotnet restore
dotnet build
dotnet run
```

## Endpoints

### Utilisateurs

- `POST /api/users/register` : Créer un nouveau compte utilisateur
- `POST /api/users/login` : Connexion de l'utilisateur

### Locations

- `GET /api/locations` : Récupérer toutes les locations
- `GET /api/locations/{id}` : Récupérer une location par ID
- `POST /api/locations` : Créer une nouvelle location
  - Paramètres attendus : 
    - `name`: string
    - `description`: string
    - `availableFrom`: DateTime
    - `availableTo`: DateTime
    - `maxGuests`: int
    - `includesTransport`: bool
    - `price`: decimal
    - `city`: string
    - `image`: IFormFile (fichier image)
- `GET /api/locations/search` : Rechercher des locations
  - Paramètres facultatifs :
    - `searchTerm`: string
    - `availableFrom`: DateTime?
    - `availableTo`: DateTime?
    - `maxPrice`: decimal?
    - `maxGuests`: int?

### Vols

- `GET /api/flights` : Récupérer tous les vols
- `GET /api/flights/{id}` : Récupérer un vol par ID

### Réservations

- `GET /api/reservations` : Récupérer toutes les réservations
- `GET /api/reservations/{id}` : Récupérer une réservation par ID
- `POST /api/reservations` : Créer une nouvelle réservation
  - Paramètres attendus : 
    - `startDate`: DateTime
    - `endDate`: DateTime
    - `numberOfGuests`: int
    - `totalPrice`: decimal
    - `userId`: int
    - `locationId`: int
    - `flights`: List<int> (IDs des vols)
    - `hotels`: List<int> (IDs des hôtels)
- `PUT /api/reservations/{id}` : Mettre à jour une réservation
  - Paramètres attendus : 
    - `startDate`: DateTime
    - `endDate`: DateTime
    - `numberOfGuests`: int
    - `totalPrice`: decimal
    - `status`: string
    - `userId`: int
    - `locationId`: int
    - `flights`: List<int> (IDs des vols)
    - `hotels`: List<int> (IDs des hôtels)
- `DELETE /api/reservations/{id}` : Supprimer une réservation

### Paiements

- `POST /api/payments/{reservationId}/pay` : Payer une réservation
  - Paramètres attendus :
    - `token`: string
    - `amount`: decimal
    - `currency`: string

## Utilisation avec Angular/Ionic

Pour intégrer cette API avec une application Angular/Ionic, suivez ces étapes :

1. **Installer les dépendances nécessaires :**

   ```bash
   npm install @angular/common @angular/core @angular/http
   npm install stripe
   ```

2. **Configurer les services Angular :**

   Créez un service Angular pour gérer les appels à l'API.

   ```typescript
   import { Injectable } from '@angular/core';
   import { HttpClient, HttpHeaders } from '@angular/common/http';
   import { Observable } from 'rxjs';

   @Injectable({
     providedIn: 'root'
   })
   export class ApiService {
     private apiUrl = 'https://localhost:32774/api'; // Remplacez par l'URL de votre API

     constructor(private http: HttpClient) {}

     // Méthodes pour les appels API
     getLocations(): Observable<any> {
       return this.http.get(`${this.apiUrl}/locations`);
     }

     getLocationById(id: number): Observable<any> {
       return this.http.get(`${this.apiUrl}/locations/${id}`);
     }

     createLocation(location: any, image: File): Observable<any> {
       const formData = new FormData();
       formData.append('name', location.name);
       formData.append('description', location.description);
       formData.append('availableFrom', location.availableFrom);
       formData.append('availableTo', location.availableTo);
       formData.append('maxGuests', location.maxGuests);
       formData.append('includesTransport', location.includesTransport);
       formData.append('price', location.price);
       formData.append('city', location.city);
       formData.append('image', image);

       return this.http.post(`${this.apiUrl}/locations`, formData);
     }

     // Ajoutez des méthodes similaires pour les vols, les réservations et les paiements
   }
   ```

3. **Utilisation des services dans les composants :**

   Injectez le service dans vos composants et utilisez-le pour effectuer des appels API.

   ```typescript
   import { Component, OnInit } from '@angular/core';
   import { ApiService } from '../services/api.service';

   @Component({
     selector: 'app-locations',
     templateUrl: './locations.page.html',
     styleUrls: ['./locations.page.scss'],
   })
   export class LocationsPage implements OnInit {
     locations: any[] = [];

     constructor(private apiService: ApiService) {}

     ngOnInit() {
       this.apiService.getLocations().subscribe((data: any[]) => {
         this.locations = data;
       });
     }
   }
   ```