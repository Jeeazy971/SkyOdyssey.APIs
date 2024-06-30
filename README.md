# SkyOdyssey API

SkyOdyssey est une API con�ue pour g�rer les r�servations de vols et d'h�tels, permettant aux utilisateurs de rechercher, r�server et payer pour des voyages de mani�re efficace. Cette API prend en charge l'int�gration avec Stripe pour le traitement des paiements.

## Fonctionnalit�s

### Gestion des Utilisateurs
- **Cr�ation de compte utilisateur**
- **Connexion utilisateur**
- **Gestion des informations de l'utilisateur**

### Gestion des Locations
- **Cr�ation de location avec image**
- **Recherche de locations par ville, date de disponibilit�, prix, et nombre maximum d'invit�s**
- **R�cup�ration de toutes les locations**
- **R�cup�ration d'une location par ID**

### Gestion des Vols
- **R�cup�ration de tous les vols**
- **R�cup�ration d'un vol par ID**

### Gestion des R�servations
- **Cr�ation de r�servation**
- **R�cup�ration de toutes les r�servations**
- **R�cup�ration d'une r�servation par ID**
- **Mise � jour d'une r�servation**
- **Suppression d'une r�servation**

### Paiements
- **Traitement des paiements via Stripe**

## Pr�requis

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

Cr�ez un fichier `appsettings.json` dans le r�pertoire du projet avec le contenu suivant :

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

Cr�ez un fichier `Dockerfile` � la racine du projet avec le contenu suivant :

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

### Ex�cuter le projet avec Docker

```bash
docker build -t skyodyssey-api .
```

### Ex�cuter le projet localement

Ouvrez le projet dans Visual Studio Code, puis ex�cutez les commandes suivantes dans le terminal int�gr� :

```bash
dotnet restore
dotnet build
dotnet run
```

## Endpoints

### Utilisateurs

- `POST /api/users/register` : Cr�er un nouveau compte utilisateur
- `POST /api/users/login` : Connexion de l'utilisateur

### Locations

- `GET /api/locations` : R�cup�rer toutes les locations
- `GET /api/locations/{id}` : R�cup�rer une location par ID
- `POST /api/locations` : Cr�er une nouvelle location
  - Param�tres attendus : 
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
  - Param�tres facultatifs :
    - `searchTerm`: string
    - `availableFrom`: DateTime?
    - `availableTo`: DateTime?
    - `maxPrice`: decimal?
    - `maxGuests`: int?

### Vols

- `GET /api/flights` : R�cup�rer tous les vols
- `GET /api/flights/{id}` : R�cup�rer un vol par ID

### R�servations

- `GET /api/reservations` : R�cup�rer toutes les r�servations
- `GET /api/reservations/{id}` : R�cup�rer une r�servation par ID
- `POST /api/reservations` : Cr�er une nouvelle r�servation
  - Param�tres attendus : 
    - `startDate`: DateTime
    - `endDate`: DateTime
    - `numberOfGuests`: int
    - `totalPrice`: decimal
    - `userId`: int
    - `locationId`: int
    - `flights`: List<int> (IDs des vols)
    - `hotels`: List<int> (IDs des h�tels)
- `PUT /api/reservations/{id}` : Mettre � jour une r�servation
  - Param�tres attendus : 
    - `startDate`: DateTime
    - `endDate`: DateTime
    - `numberOfGuests`: int
    - `totalPrice`: decimal
    - `status`: string
    - `userId`: int
    - `locationId`: int
    - `flights`: List<int> (IDs des vols)
    - `hotels`: List<int> (IDs des h�tels)
- `DELETE /api/reservations/{id}` : Supprimer une r�servation

### Paiements

- `POST /api/payments/{reservationId}/pay` : Payer une r�servation
  - Param�tres attendus :
    - `token`: string
    - `amount`: decimal
    - `currency`: string

## Utilisation avec Angular/Ionic

Pour int�grer cette API avec une application Angular/Ionic, suivez ces �tapes :

1. **Installer les d�pendances n�cessaires :**

   ```bash
   npm install @angular/common @angular/core @angular/http
   npm install stripe
   ```

2. **Configurer les services Angular :**

   Cr�ez un service Angular pour g�rer les appels � l'API.

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

     // M�thodes pour les appels API
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

     // Ajoutez des m�thodes similaires pour les vols, les r�servations et les paiements
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