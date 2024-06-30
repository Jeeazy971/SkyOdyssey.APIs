## SkyOdyssey API Documentation

### Introduction

SkyOdyssey API est une API qui permet de g�rer des r�servations de voyages incluant des vols et des h�tels. Cette documentation couvre l'ensemble des fonctionnalit�s offertes par l'API, ainsi que des exemples de requ�tes pour chaque endpoint. De plus, un guide pour l'utilisation avec un front-end Angular/Ionic est inclus.

### Fonctionnalit�s

- Gestion des utilisateurs (enregistrement, authentification)
- Gestion des locations (cr�ation, r�cup�ration, recherche)
- Gestion des r�servations (cr�ation, mise � jour, suppression)
- Gestion des vols (affectation � des r�servations)
- Paiement via Stripe

### Configuration et Installation

#### Pr�requis

- .NET 8 SDK
- Visual Studio Code ou tout autre IDE compatible .NET
- Docker
- SQLite

#### Cloner le d�p�t

```bash
git clone https://github.com/votre-utilisateur/skyodyssey-api.git
cd skyodyssey-api
```

#### Configuration de la base de donn�es

La base de donn�es utilis�e est SQLite. Assurez-vous que la cha�ne de connexion est correctement configur�e dans `appsettings.json`.

#### Migration de la base de donn�es

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### D�marrage de l'application

```bash
dotnet run
```

L'application sera accessible � l'adresse `https://localhost:5001`.

### Endpoints

#### Utilisateurs

- **Enregistrement**

  - **Endpoint**: `/api/users/register`
  - **M�thode**: POST
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
  - **M�thode**: POST
  - **Payload**:
    ```json
    {
      "username": "string",
      "password": "string"
    }
    ```

#### Locations

- **R�cup�rer toutes les locations**

  - **Endpoint**: `/api/locations`
  - **M�thode**: GET

- **R�cup�rer une location par ID**

  - **Endpoint**: `/api/locations/{id}`
  - **M�thode**: GET

- **Cr�er une location**

  - **Endpoint**: `/api/locations`
  - **M�thode**: POST
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
  - **M�thode**: GET
  - **Query Parameters**:
    - `searchTerm`: string
    - `availableFrom`: DateTime
    - `availableTo`: DateTime
    - `maxPrice`: decimal
    - `maxGuests`: int

#### R�servations

- **R�cup�rer toutes les r�servations**

  - **Endpoint**: `/api/reservations`
  - **M�thode**: GET

- **R�cup�rer une r�servation par ID**

  - **Endpoint**: `/api/reservations/{id}`
  - **M�thode**: GET

- **Cr�er une r�servation**

  - **Endpoint**: `/api/reservations`
  - **M�thode**: POST
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

- **Mettre � jour une r�servation**

  - **Endpoint**: `/api/reservations/{id}`
  - **M�thode**: PUT
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

- **Supprimer une r�servation**

  - **Endpoint**: `/api/reservations/{id}`
  - **M�thode**: DELETE

#### Paiements

- **Effectuer un paiement**

  - **Endpoint**: `/api/payments/{reservationId}/pay`
  - **M�thode**: POST
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

1. **Installer les d�pendances n�cessaires**

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

      // R�servations
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

#### Exemple de composant pour les r�servations

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