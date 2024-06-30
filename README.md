## SkyOdyssey API Documentation

### Introduction

SkyOdyssey API est une API qui permet de gérer des réservations de voyages incluant des vols et des hôtels. Cette documentation couvre l'ensemble des fonctionnalités offertes par l'API, ainsi que des exemples de requêtes pour chaque endpoint. De plus, un guide pour l'utilisation avec un front-end Angular/Ionic est inclus.

### Fonctionnalités

- Gestion des utilisateurs (enregistrement, authentification)
- Gestion des locations (création, récupération, recherche)
- Gestion des réservations (création, mise à jour, suppression)
- Gestion des vols (affectation à des réservations)
- Paiement via Stripe

### Configuration et Installation

#### Prérequis

- .NET 8 SDK
- Visual Studio Code ou tout autre IDE compatible .NET
- Docker
- SQLite

#### Cloner le dépôt

```bash
git clone https://github.com/votre-utilisateur/skyodyssey-api.git
cd skyodyssey-api
```

#### Configuration de la base de données

La base de données utilisée est SQLite. Assurez-vous que la chaîne de connexion est correctement configurée dans `appsettings.json`.

#### Migration de la base de données

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### Démarrage de l'application

```bash
dotnet run
```

L'application sera accessible à l'adresse `https://localhost:5001`.

### Endpoints

#### Utilisateurs

- **Enregistrement**

  - **Endpoint**: `/api/users/register`
  - **Méthode**: POST
  - **Payload**:
    ```json
    {
      "username": "string",
      "email": "string",
      "password": "string"
    }
    ```

- **Authentification**

  - **Endpoint**: `/api/users/login`
  - **Méthode**: POST
  - **Payload**:
    ```json
    {
      "username": "string",
      "password": "string"
    }
    ```

#### Locations

- **Récupérer toutes les locations**

  - **Endpoint**: `/api/locations`
  - **Méthode**: GET

- **Récupérer une location par ID**

  - **Endpoint**: `/api/locations/{id}`
  - **Méthode**: GET

- **Créer une location**

  - **Endpoint**: `/api/locations`
  - **Méthode**: POST
  - **Payload**:
    ```json
    {
      "name": "string",
      "description": "string",
      "availableFrom": "2023-08-24T09:27:02.7352395",
      "availableTo": "2025-01-31T20:24:15.5929888",
      "maxGuests": 7,
      "includesTransport": false,
      "price": 2201.84,
      "city": "string",
      "imagePath": "string"
    }
    ```

- **Rechercher des locations**

  - **Endpoint**: `/api/locations/search`
  - **Méthode**: GET
  - **Query Parameters**:
    - `searchTerm`: string
    - `availableFrom`: DateTime
    - `availableTo`: DateTime
    - `maxPrice`: decimal
    - `maxGuests`: int

#### Réservations

- **Récupérer toutes les réservations**

  - **Endpoint**: `/api/reservations`
  - **Méthode**: GET

- **Récupérer une réservation par ID**

  - **Endpoint**: `/api/reservations/{id}`
  - **Méthode**: GET

- **Créer une réservation**

  - **Endpoint**: `/api/reservations`
  - **Méthode**: POST
  - **Payload**:
    ```json
    {
      "startDate": "2023-08-24T09:27:02.7352395",
      "endDate": "2025-01-31T20:24:15.5929888",
      "numberOfGuests": 7,
      "totalPrice": 2201.84,
      "userId": 1,
      "locationId": 1,
      "flights": [
        {
          "id": 1,
          "flightNumber": "BI63U",
          "departureAirport": "string",
          "arrivalAirport": "string",
          "departureTime": "2024-08-16T08:55:54.8117161",
          "arrivalTime": "2024-12-11T11:28:54.2639328",
          "price": 1153.32,
          "airline": "string",
          "locationId": 1
        }
      ],
      "hotels": [
        {
          "name": "string",
          "location": "string",
          "pricePerNight": 220.1
        }
      ]
    }
    ```

- **Mettre à jour une réservation**

  - **Endpoint**: `/api/reservations/{id}`
  - **Méthode**: PUT
  - **Payload**:
    ```json
    {
      "startDate": "2023-08-24T09:27:02.7352395",
      "endDate": "2025-01-31T20:24:15.5929888",
      "numberOfGuests": 7,
      "totalPrice": 2201.84,
      "userId": 1,
      "locationId": 1,
      "status": "string",
      "flights": [
        {
          "id": 1,
          "flightNumber": "BI63U",
          "departureAirport": "string",
          "arrivalAirport": "string",
          "departureTime": "2024-08-16T08:55:54.8117161",
          "arrivalTime": "2024-12-11T11:28:54.2639328",
          "price": 1153.32,
          "airline": "string",
          "locationId": 1
        }
      ],
      "hotels": [
        {
          "name": "string",
          "location": "string",
          "pricePerNight": 220.1
        }
      ]
    }
    ```

- **Supprimer une réservation**

  - **Endpoint**: `/api/reservations/{id}`
  - **Méthode**: DELETE

#### Paiements

- **Effectuer un paiement**

  - **Endpoint**: `/api/payments/{reservationId}/pay`
  - **Méthode**: POST
  - **Payload**:
    ```json
    {
      "token": "string",
      "amount": 500,
      "currency": "eur",
      "reservationId": 3
    }
    ```

### Utilisation avec Angular/Ionic

#### Configuration

1. **Installer les dépendances nécessaires**

    ```bash
    npm install @ionic/angular @angular/http
    ```

2. **Service Angular pour interagir avec l'API**

    ```typescript
    import { Injectable } from '@angular/core';
    import { HttpClient, HttpHeaders } from '@angular/common/http';
    import { Observable } from 'rxjs';

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    @Injectable({
      providedIn: 'root'
    })
    export class ApiService {
      private apiUrl = 'https://localhost:5001/api';

      constructor(private http: HttpClient) {}

      // Utilisateurs
      register(user: any): Observable<any> {
        return this.http.post(`${this.apiUrl}/users/register`, user, httpOptions);
      }

      login(credentials: any): Observable<any> {
        return this.http.post(`${this.apiUrl}/users/login`, credentials, httpOptions);
      }

      // Locations
      getLocations(): Observable<any> {
        return this.http.get(`${this.apiUrl}/locations`, httpOptions);
      }

      getLocationById(id: number): Observable<any> {
        return this.http.get(`${this.apiUrl}/locations/${id}`, httpOptions);
      }

      createLocation(location: any): Observable<any> {
        return this.http.post(`${this.apiUrl}/locations`, location, httpOptions);
      }

      searchLocations(searchTerm: string, availableFrom: string, availableTo: string, maxPrice: number, maxGuests: number): Observable<any> {
        return this.http.get(`${this.apiUrl}/locations/search?searchTerm=${searchTerm}&availableFrom=${availableFrom}&availableTo=${availableTo}&maxPrice=${

maxPrice}&maxGuests=${maxGuests}`, httpOptions);
      }

      // Réservations
      getReservations(): Observable<any> {
        return this.http.get(`${this.apiUrl}/reservations`, httpOptions);
      }

      getReservationById(id: number): Observable<any> {
        return this.http.get(`${this.apiUrl}/reservations/${id}`, httpOptions);
      }

      createReservation(reservation: any): Observable<any> {
        return this.http.post(`${this.apiUrl}/reservations`, reservation, httpOptions);
      }

      updateReservation(id: number, reservation: any): Observable<any> {
        return this.http.put(`${this.apiUrl}/reservations/${id}`, reservation, httpOptions);
      }

      deleteReservation(id: number): Observable<any> {
        return this.http.delete(`${this.apiUrl}/reservations/${id}`, httpOptions);
      }

      // Paiements
      pay(reservationId: number, payment: any): Observable<any> {
        return this.http.post(`${this.apiUrl}/payments/${reservationId}/pay`, payment, httpOptions);
      }
    }
    ```

#### Exemple de composant pour les réservations

```typescript
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.page.html',
  styleUrls: ['./reservations.page.scss'],
})
export class ReservationsPage implements OnInit {
  reservations: any = [];

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.loadReservations();
  }

  loadReservations() {
    this.apiService.getReservations().subscribe(data => {
      this.reservations = data;
    });
  }

  deleteReservation(id: number) {
    this.apiService.deleteReservation(id).subscribe(() => {
      this.loadReservations();
    });
  }
}
```