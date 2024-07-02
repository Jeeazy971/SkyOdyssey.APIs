### Documentation de l'API SkyOdyssey

---

### À quoi sert l'API

L'API SkyOdyssey est conçue pour gérer les réservations de voyages incluant des vols et des hébergements. Elle permet aux utilisateurs de créer, lire, mettre à jour et supprimer des réservations. De plus, elle facilite la gestion des utilisateurs, des vols et des locations.

### Comment utiliser l'API

Pour utiliser l'API SkyOdyssey, vous devez d'abord vous authentifier en tant qu'utilisateur. Une fois authentifié, vous pouvez accéder aux différentes fonctionnalités de l'API via les endpoints décrits ci-dessous.

### L'architecture du projet

L'architecture du projet suit le modèle classique de séparation des préoccupations avec les couches suivantes :
- **Controllers** : Gestion des requêtes HTTP et des réponses.
- **Services** : Logique métier.
- **Repositories** : Accès aux données.
- **Models** : Modèles de données.
- **DTOs (Data Transfer Objects)** : Objets utilisés pour transférer des données entre les couches.

### Endpoints de l'API

Voici une liste détaillée des endpoints disponibles dans l'API SkyOdyssey :

#### Utilisateurs

- **Register User**
  - **URL** : `/api/Users/register`
  - **Méthode** : `POST`
  - **Description** : Enregistre un nouvel utilisateur.
  - **Body** : 
    ```json
    {
      "username": "string",
      "email": "string",
      "password": "string"
    }
    ```
  - **Response** : 
    ```json
    {
      "id": 0,
      "username": "string",
      "email": "string",
      "token": "string"
    }
    ```

- **Authenticate User**
  - **URL** : `/api/Users/authenticate`
  - **Méthode** : `POST`
  - **Description** : Authentifie un utilisateur.
  - **Body** : 
    ```json
    {
      "email": "string",
      "password": "string"
    }
    ```
  - **Response** : 
    ```json
    {
      "id": 0,
      "username": "string",
      "email": "string",
      "token": "string"
    }
    ```

#### Réservations

- **Get All Reservations**
  - **URL** : `/api/Reservations`
  - **Méthode** : `GET`
  - **Description** : Récupère toutes les réservations.
  - **Response** : 
    ```json
    [
      {
        "id": 0,
        "startDate": "2024-07-01T21:58:33.783Z",
        "endDate": "2024-07-01T21:58:33.783Z",
        "numberOfGuests": 0,
        "totalPrice": 0,
        "userId": 0,
        "flights": [
          {
            "id": 0,
            "flightNumber": "string",
            "departureAirport": "string",
            "arrivalAirport": "string",
            "departureTime": "2024-07-01T21:58:33.783Z",
            "arrivalTime": "2024-07-01T21:58:33.783Z",
            "price": 0,
            "airline": "string"
          }
        ],
        "locations": [
          {
            "id": 0,
            "name": "string",
            "description": "string",
            "availableFrom": "2024-07-01T21:58:33.783Z",
            "availableTo": "2024-07-01T21:58:33.783Z",
            "maxGuests": 0,
            "includesTransport": true,
            "price": 0,
            "city": "string",
            "imagePath": "string"
          }
        ]
      }
    ]
    ```

- **Get Reservation By ID**
  - **URL** : `/api/Reservations/{id}`
  - **Méthode** : `GET`
  - **Description** : Récupère une réservation par ID.
  - **Response** : 
    ```json
    {
      "id": 0,
      "startDate": "2024-07-01T21:58:33.783Z",
      "endDate": "2024-07-01T21:58:33.783Z",
      "numberOfGuests": 0,
      "totalPrice": 0,
      "userId": 0,
      "flights": [
        {
          "id": 0,
          "flightNumber": "string",
          "departureAirport": "string",
          "arrivalAirport": "string",
          "departureTime": "2024-07-01T21:58:33.783Z",
          "arrivalTime": "2024-07-01T21:58:33.783Z",
          "price": 0,
          "airline": "string"
        }
      ],
      "locations": [
        {
          "id": 0,
          "name": "string",
          "description": "string",
          "availableFrom": "2024-07-01T21:58:33.783Z",
          "availableTo": "2024-07-01T21:58:33.783Z",
          "maxGuests": 0,
          "includesTransport": true,
          "price": 0,
          "city": "string",
          "imagePath": "string"
        }
      ]
    }
    ```

- **Create Reservation**
  - **URL** : `/api/Reservations`
  - **Méthode** : `POST`
  - **Description** : Crée une nouvelle réservation.
  - **Body** : 
    ```json
    {
      "startDate": "2024-07-01T21:58:33.783Z",
      "endDate": "2024-07-01T21:58:33.783Z",
      "numberOfGuests": 2,
      "totalPrice": 500,
      "userId": 1,
      "locationIds": [1],
      "flightIds": [1]
    }
    ```
  - **Response** : `201 Created`

- **Update Reservation**
  - **URL** : `/api/Reservations/{id}`
  - **Méthode** : `PUT`
  - **Description** : Met à jour une réservation existante.
  - **Body** : 
    ```json
    {
      "id": 1,
      "startDate": "2024-07-01T21:58:33.783Z",
      "endDate": "2024-07-01T21:58:33.783Z",
      "numberOfGuests": 2,
      "totalPrice": 500,
      "userId": 1,
      "locationIds": [1],
      "flightIds": [1]
    }
    ```
  - **Response** : `204 No Content`

- **Delete Reservation**
  - **URL** : `/api/Reservations/{id}`
  - **Méthode** : `DELETE`
  - **Description** : Supprime une réservation par ID.
  - **Response** : `204 No Content`

#### Vols

- **Get All Flights**
  - **URL** : `/api/Flights`
  - **Méthode** : `GET`
  - **Description** : Récupère tous les vols.
  - **Response** : 
    ```json
    [
      {
        "id": 0,
        "flightNumber": "string",
        "departureAirport": "string",
        "arrivalAirport": "string",
        "departureTime": "2024-07-01T21:58:33.783Z",
        "arrivalTime": "2024-07-01T21:58:33.783Z",
        "price": 0,
        "airline": "string"
      }
    ]
    ```

- **Get Available Flights**
  - **URL** : `/api/Flights/available`
  - **Méthode** : `GET`
  - **Description** : Récupère les vols disponibles.
  - **Response** : 
    ```json
    [
      {
        "id": 0,
        "flightNumber": "string",
        "departureAirport": "string",
        "arrivalAirport": "string",
        "departureTime": "2024-07-01T21:58:33.783Z",
        "arrivalTime": "2024-07-01T21:58:33.783Z",
        "price": 0,
        "airline": "string"
      }
    ]
    ```

#### Locations

- **Get All Locations**
  - **URL** : `/api/Locations`
  - **Méthode** : `GET`
  - **Description** : Récupère toutes les locations.
  - **Response** : 
    ```json
    [
      {
        "id": 0,
        "name": "string",
        "description": "string",
        "availableFrom": "2024-07-01T21:58:33.783Z",
        "availableTo": "2024-07-01T21:58:33.783Z",
        "maxGuests": 0,
        "includesTransport": true,
        "price": 0,
        "city": "string",
        "imagePath":

 "string"
      }
    ]
    ```

- **Get Available Locations**
  - **URL** : `/api/Locations/available`
  - **Méthode** : `GET`
  - **Description** : Récupère les locations disponibles.
  - **Response** : 
    ```json
    [
      {
        "id": 0,
        "name": "string",
        "description": "string",
        "availableFrom": "2024-07-01T21:58:33.783Z",
        "availableTo": "2024-07-01T21:58:33.783Z",
        "maxGuests": 0,
        "includesTransport": true,
        "price": 0,
        "city": "string",
        "imagePath": "string"
      }
    ]
    ```

### Fonctionnalités de l'API

- Gestion des utilisateurs (inscription, authentification).
- Gestion des réservations (CRUD).
- Gestion des vols (récupération des vols disponibles).
- Gestion des locations (récupération des locations disponibles).
- Paiement des réservations.

### Utilisation des Endpoints côté Front avec Ionic/Angular

Pour interagir avec l'API depuis une application Ionic/Angular, voici un exemple de service Angular pour gérer les réservations :

**reservation.service.ts** :
```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private apiUrl = `${environment.apiUrl}/api/Reservations`;

  constructor(private http: HttpClient) { }

  getReservations(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getReservationById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  createReservation(reservation: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, reservation);
  }

  updateReservation(id: number, reservation: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, reservation);
  }

  deleteReservation(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
```

**reservation.component.ts** :
```typescript
import { Component, OnInit } from '@angular/core';
import { ReservationService } from './reservation.service';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css']
})
export class ReservationComponent implements OnInit {
  reservations: any[] = [];
  selectedReservation: any = null;

  constructor(private reservationService: ReservationService) { }

  ngOnInit(): void {
    this.loadReservations();
  }

  loadReservations(): void {
    this.reservationService.getReservations().subscribe(data => {
      this.reservations = data;
    });
  }

  selectReservation(id: number): void {
    this.reservationService.getReservationById(id).subscribe(data => {
      this.selectedReservation = data;
    });
  }

  createReservation(): void {
    const newReservation = {
      startDate: new Date(),
      endDate: new Date(),
      numberOfGuests: 2,
      totalPrice: 500,
      userId: 1,
      locationIds: [1],
      flightIds: [1]
    };
    this.reservationService.createReservation(newReservation).subscribe(() => {
      this.loadReservations();
    });
  }

  updateReservation(): void {
    if (this.selectedReservation) {
      this.reservationService.updateReservation(this.selectedReservation.id, this.selectedReservation).subscribe(() => {
        this.loadReservations();
      });
    }
  }

  deleteReservation(id: number): void {
    this.reservationService.deleteReservation(id).subscribe(() => {
      this.loadReservations();
    });
  }
}
```