# Documentation de l'API SkyOdyssey

## Introduction
SkyOdyssey est une API de réservation de voyages qui permet aux utilisateurs de rechercher des hôtels, de réserver des vols et des hôtels, et de gérer leurs réservations. Cette documentation couvre tous les endpoints disponibles dans l'API et comment les utiliser côté front avec Angular/Ionic.

## Configuration de l'API

### CORS
Pour permettre à l'API d'accepter des requêtes provenant de n'importe quelle origine, assurez-vous que le middleware CORS est correctement configuré dans `Program.cs`.

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
```

## Endpoints de l'API

### Utilisateurs

#### Enregistrer un utilisateur
```
POST /api/users/register
```
**Description :** Enregistre un nouvel utilisateur.

**Exemple de requête :**
```json
{
  "username": "john_doe",
  "email": "john@example.com",
  "password": "password123"
}
```

**Réponse :**
```json
{
  "id": 1,
  "username": "john_doe",
  "email": "john@example.com"
}
```

#### Authentifier un utilisateur
```
POST /api/users/login
```
**Description :** Authentifie un utilisateur et renvoie un jeton JWT.

**Exemple de requête :**
```json
{
  "username": "john_doe",
  "password": "password123"
}
```

**Réponse :**
```json
{
  "token": "jwt-token-string"
}
```

#### Récupérer tous les utilisateurs
```
GET /api/users
```
**Description :** Récupère la liste de tous les utilisateurs.

**Réponse :**
```json
[
  {
    "id": 1,
    "username": "john_doe",
    "email": "john@example.com"
  }
]
```

#### Récupérer un utilisateur par ID
```
GET /api/users/{id}
```
**Description :** Récupère un utilisateur par son ID.

**Réponse :**
```json
{
  "id": 1,
  "username": "john_doe",
  "email": "john@example.com"
}
```

### Locations (Hôtels)

#### Récupérer toutes les locations
```
GET /api/locations
```
**Description :** Récupère la liste de toutes les locations.

**Réponse :**
```json
[
  {
    "id": 1,
    "name": "Paris Hotel",
    "description": "Profitez de notre hôtel Paris Hotel situé au cœur de Paris...",
    "availableFrom": "2023-01-01T00:00:00",
    "availableTo": "2023-12-31T23:59:59",
    "maxGuests": 4,
    "includesTransport": true,
    "price": 150.00,
    "city": "Paris",
    "imagePath": "uploads/abc123.jpg"
  }
]
```

#### Récupérer une location par ID
```
GET /api/locations/{id}
```
**Description :** Récupère une location par son ID.

**Réponse :**
```json
{
  "id": 1,
  "name": "Paris Hotel",
  "description": "Profitez de notre hôtel Paris Hotel situé au cœur de Paris...",
  "availableFrom": "2023-01-01T00:00:00",
  "availableTo": "2023-12-31T23:59:59",
  "maxGuests": 4,
  "includesTransport": true,
  "price": 150.00,
  "city": "Paris",
  "imagePath": "uploads/abc123.jpg"
}
```

#### Créer une location
```
POST /api/locations
```
**Description :** Crée une nouvelle location.

**Exemple de requête :**
```json
{
  "name": "Paris Hotel",
  "description": "Profitez de notre hôtel Paris Hotel situé au cœur de Paris...",
  "availableFrom": "2023-01-01T00:00:00",
  "availableTo": "2023-12-31T23:59:59",
  "maxGuests": 4,
  "includesTransport": true,
  "price": 150.00,
  "city": "Paris",
  "image": "image-file"
}
```

**Réponse :**
```json
{
  "id": 1,
  "name": "Paris Hotel",
  "description": "Profitez de notre hôtel Paris Hotel situé au cœur de Paris...",
  "availableFrom": "2023-01-01T00:00:00",
  "availableTo": "2023-12-31T23:59:59",
  "maxGuests": 4,
  "includesTransport": true,
  "price": 150.00,
  "city": "Paris",
  "imagePath": "uploads/abc123.jpg"
}
```

#### Mettre à jour une location
```
PUT /api/locations/{id}
```
**Description :** Met à jour une location existante.

**Exemple de requête :**
```json
{
  "name": "Updated Paris Hotel",
  "description": "Profitez de notre hôtel Paris Hotel situé au cœur de Paris...",
  "availableFrom": "2023-01-01T00:00:00",
  "availableTo": "2023-12-31T23:59:59",
  "maxGuests": 4,
  "includesTransport": true,
  "price": 150.00,
  "city": "Paris",
  "imagePath": "uploads/abc123.jpg"
}
```

**Réponse :**
```json
{}
```

#### Supprimer une location
```
DELETE /api/locations/{id}
```
**Description :** Supprime une location par son ID.

**Réponse :**
```json
{}
```

#### Rechercher des locations
```
GET /api/locations/search
```
**Description :** Recherche des locations basées sur différents critères.

**Exemple de requête :**
```
GET /api/locations/search?searchTerm=Paris&availableFrom=2023-01-01&availableTo=2023-12-31&maxPrice=200&maxGuests=4
```

**Réponse :**
```json
[
  {
    "id": 1,
    "name": "Paris Hotel",
    "description": "Profitez de notre hôtel Paris Hotel situé au cœur de Paris...",
    "availableFrom": "2023-01-01T00:00:00",
    "availableTo": "2023-12-31T23:59:59",
    "maxGuests": 4,
    "includesTransport": true,
    "price": 150.00,
    "city": "Paris",
    "imagePath": "uploads/abc123.jpg"
  }
]
```

### Réservations

#### Récupérer toutes les réservations
```
GET /api/reservations
```
**Description :** Récupère la liste de toutes les réservations.

**Réponse :**
```json
[
  {
    "id": 1,
    "startDate": "2023-06-01T00:00:00",
    "endDate": "2023-06-10T00:00:00",
    "numberOfGuests": 2,
    "totalPrice": 300.00,
    "userId": 1,
    "locationId": 1,
    "status": "Pending",
    "flights": [],
    "hotels": []
  }
]
```

#### Récupérer une réservation par ID
```
GET /api/reservations/{id}
```
**Description :** Récupère une réservation par son ID.

**Réponse :**
```json
{
  "id": 1,
  "startDate": "2023-06-01T00:00:00",
    "endDate": "2023-06-10T00:00:00",
    "numberOfGuests": 2,
    "totalPrice": 300.00,
    "userId": 1,
    "locationId": 1,
    "status": "Pending",
    "flights": [],
    "hotels": []
}
```

#### Créer une réservation
```
POST /api/reservations
```
**Description :** Crée une nouvelle réservation.

**Exemple de requête :**
```json
{
  "startDate": "2023-06-01T00:00:00",
  "endDate": "2023-06-10T00:00:00",
  "numberOfGuests": 2,
  "totalPrice": 300.00,
  "userId": 1,
  "locationId": 1,
  "flights": [],
  "hotels": []
}
```

**Réponse :**
```json
{
  "id": 1,
  "startDate": "2023-06-01T00:00:00",
    "endDate": "2023-06-10T00:00:00",
    "numberOfGuests": 2,
    "totalPrice": 300.00,
    "userId": 1

,
    "locationId": 1,
    "status": "Pending",
    "flights": [],
    "hotels": []
}
```

#### Mettre à jour une réservation
```
PUT /api/reservations/{id}
```
**Description :** Met à jour une réservation existante.

**Exemple de requête :**
```json
{
  "startDate": "2023-06-01T00:00:00",
  "endDate": "2023-06-10T00:00:00",
  "numberOfGuests": 2,
  "totalPrice": 300.00,
  "userId": 1,
  "locationId": 1,
  "status": "Confirmed",
  "flights": [],
  "hotels": []
}
```

**Réponse :**
```json
{}
```

#### Supprimer une réservation
```
DELETE /api/reservations/{id}
```
**Description :** Supprime une réservation par son ID.

**Réponse :**
```json
{}
```

### Vols

#### Récupérer tous les vols
```
GET /api/flights
```
**Description :** Récupère la liste de tous les vols.

**Réponse :**
```json
[
  {
    "id": 1,
    "flightNumber": "AF123",
    "departureAirport": "Paris Charles de Gaulle",
    "arrivalAirport": "New York JFK",
    "departureTime": "2023-06-01T10:00:00",
    "arrivalTime": "2023-06-01T13:00:00",
    "price": 500.00,
    "airline": "Air France",
    "reservationId": 1,
    "locationId": 1
  }
]
```

#### Récupérer un vol par ID
```
GET /api/flights/{id}
```
**Description :** Récupère un vol par son ID.

**Réponse :**
```json
{
  "id": 1,
  "flightNumber": "AF123",
  "departureAirport": "Paris Charles de Gaulle",
  "arrivalAirport": "New York JFK",
  "departureTime": "2023-06-01T10:00:00",
  "arrivalTime": "2023-06-01T13:00:00",
  "price": 500.00,
  "airline": "Air France",
  "reservationId": 1,
  "locationId": 1
}
```

#### Créer un vol
```
POST /api/flights
```
**Description :** Crée un nouveau vol.

**Exemple de requête :**
```json
{
  "flightNumber": "AF123",
  "departureAirport": "Paris Charles de Gaulle",
  "arrivalAirport": "New York JFK",
  "departureTime": "2023-06-01T10:00:00",
  "arrivalTime": "2023-06-01T13:00:00",
  "price": 500.00,
  "airline": "Air France",
  "reservationId": 1,
  "locationId": 1
}
```

**Réponse :**
```json
{
  "id": 1,
  "flightNumber": "AF123",
  "departureAirport": "Paris Charles de Gaulle",
  "arrivalAirport": "New York JFK",
  "departureTime": "2023-06-01T10:00:00",
  "arrivalTime": "2023-06-01T13:00:00",
  "price": 500.00,
  "airline": "Air France",
  "reservationId": 1,
  "locationId": 1
}
```

#### Mettre à jour un vol
```
PUT /api/flights/{id}
```
**Description :** Met à jour un vol existant.

**Exemple de requête :**
```json
{
  "flightNumber": "AF123",
  "departureAirport": "Paris Charles de Gaulle",
  "arrivalAirport": "New York JFK",
  "departureTime": "2023-06-01T10:00:00",
  "arrivalTime": "2023-06-01T13:00:00",
  "price": 500.00,
  "airline": "Air France",
  "reservationId": 1,
  "locationId": 1
}
```

**Réponse :**
```json
{}
```

#### Supprimer un vol
```
DELETE /api/flights/{id}
```
**Description :** Supprime un vol par son ID.

**Réponse :**
```json
{}
```

#### Récupérer les vols par location
```
GET /api/flights/by-location/{locationId}
```
**Description :** Récupère les vols disponibles pour une location donnée.

**Réponse :**
```json
[
  {
    "id": 1,
    "flightNumber": "AF123",
    "departureAirport": "Paris Charles de Gaulle",
    "arrivalAirport": "New York JFK",
    "departureTime": "2023-06-01T10:00:00",
    "arrivalTime": "2023-06-01T13:00:00",
    "price": 500.00,
    "airline": "Air France",
    "reservationId": 1,
    "locationId": 1
  }
]
```

### Hôtels

#### Récupérer tous les hôtels
```
GET /api/hotels
```
**Description :** Récupère la liste de tous les hôtels.

**Réponse :**
```json
[
  {
    "id": 1,
    "name": "Hotel Paris",
    "location": "Paris",
    "pricePerNight": 100.00,
    "reservationId": 1
  }
]
```

#### Récupérer un hôtel par ID
```
GET /api/hotels/{id}
```
**Description :** Récupère un hôtel par son ID.

**Réponse :**
```json
{
  "id": 1,
  "name": "Hotel Paris",
  "location": "Paris",
  "pricePerNight": 100.00,
  "reservationId": 1
}
```

#### Créer un hôtel
```
POST /api/hotels
```
**Description :** Crée un nouvel hôtel.

**Exemple de requête :**
```json
{
  "name": "Hotel Paris",
  "location": "Paris",
  "pricePerNight": 100.00,
  "reservationId": 1
}
```

**Réponse :**
```json
{
  "id": 1,
  "name": "Hotel Paris",
  "location": "Paris",
  "pricePerNight": 100.00,
  "reservationId": 1
}
```

#### Mettre à jour un hôtel
```
PUT /api/hotels/{id}
```
**Description :** Met à jour un hôtel existant.

**Exemple de requête :**
```json
{
  "name": "Updated Hotel Paris",
  "location": "Paris",
  "pricePerNight": 120.00,
  "reservationId": 1
}
```

**Réponse :**
```json
{}
```

#### Supprimer un hôtel
```
DELETE /api/hotels/{id}
```
**Description :** Supprime un hôtel par son ID.

**Réponse :**
```json
{}
```

### Paiements

#### Effectuer un paiement
```
POST /api/payments/{reservationId}/pay
```
**Description :** Effectue un paiement pour une réservation donnée.

**Exemple de requête :**
```json
{
  "token": "tok_visa",
  "amount": 500,
  "currency": "eur",
  "reservationId": 1
}
```

**Réponse :**
```json
{
  "status": "succeeded",
  "paymentIntentId": "pi_1234567890"
}
```

## Intégration avec Angular/Ionic

### Configuration du service HTTP dans Angular

Créez un service Angular pour interagir avec l'API.

```typescript
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User, Location, Reservation, Flight, Hotel, Payment } from './models'; // Importez les modèles appropriés

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost:32768/api';

  constructor(private http: HttpClient) { }

  // Utilisateurs
  registerUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/users/register`, user);
  }

  loginUser(user: { username: string, password: string }): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/users/login`, user);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/users`);
  }

  getUserById(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/users/${id}`);
  }

  // Locations
  getLocations(): Observable<Location[]> {
    return this.http.get<Location[]>(`${this.apiUrl}/locations`);
  }

  getLocationById(id: number): Observable<Location>

 {
    return this.http.get<Location>(`${this.apiUrl}/locations/${id}`);
  }

  createLocation(location: FormData): Observable<Location> {
    return this.http.post<Location>(`${this.apiUrl}/locations`, location);
  }

  updateLocation(id: number, location: Location): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/locations/${id}`, location);
  }

  deleteLocation(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/locations/${id}`);
  }

  searchLocations(searchTerm: string, availableFrom?: Date, availableTo?: Date, maxPrice?: number, maxGuests?: number): Observable<Location[]> {
    let params = `?searchTerm=${searchTerm}`;
    if (availableFrom) params += `&availableFrom=${availableFrom.toISOString()}`;
    if (availableTo) params += `&availableTo=${availableTo.toISOString()}`;
    if (maxPrice) params += `&maxPrice=${maxPrice}`;
    if (maxGuests) params += `&maxGuests=${maxGuests}`;
    return this.http.get<Location[]>(`${this.apiUrl}/locations/search${params}`);
  }

  // Réservations
  getReservations(): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(`${this.apiUrl}/reservations`);
  }

  getReservationById(id: number): Observable<Reservation> {
    return this.http.get<Reservation>(`${this.apiUrl}/reservations/${id}`);
  }

  createReservation(reservation: Reservation): Observable<Reservation> {
    return this.http.post<Reservation>(`${this.apiUrl}/reservations`, reservation);
  }

  updateReservation(id: number, reservation: Reservation): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/reservations/${id}`, reservation);
  }

  deleteReservation(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/reservations/${id}`);
  }

  // Vols
  getFlights(): Observable<Flight[]> {
    return this.http.get<Flight[]>(`${this.apiUrl}/flights`);
  }

  getFlightById(id: number): Observable<Flight> {
    return this.http.get<Flight>(`${this.apiUrl}/flights/${id}`);
  }

  createFlight(flight: Flight): Observable<Flight> {
    return this.http.post<Flight>(`${this.apiUrl}/flights`, flight);
  }

  updateFlight(id: number, flight: Flight): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/flights/${id}`, flight);
  }

  deleteFlight(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/flights/${id}`);
  }

  getFlightsByLocation(locationId: number): Observable<Flight[]> {
    return this.http.get<Flight[]>(`${this.apiUrl}/flights/by-location/${locationId}`);
  }

  // Hôtels
  getHotels(): Observable<Hotel[]> {
    return this.http.get<Hotel[]>(`${this.apiUrl}/hotels`);
  }

  getHotelById(id: number): Observable<Hotel> {
    return this.http.get<Hotel>(`${this.apiUrl}/hotels/${id}`);
  }

  createHotel(hotel: Hotel): Observable<Hotel> {
    return this.http.post<Hotel>(`${this.apiUrl}/hotels`, hotel);
  }

  updateHotel(id: number, hotel: Hotel): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/hotels/${id}`, hotel);
  }

  deleteHotel(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/hotels/${id}`);
  }

  // Paiements
  makePayment(reservationId: number, payment: Payment): Observable<{ status: string, paymentIntentId: string }> {
    return this.http.post<{ status: string, paymentIntentId: string }>(`${this.apiUrl}/payments/${reservationId}/pay`, payment);
  }
}
```

### Utilisation dans un composant Angular

Exemple de composant pour afficher la liste des locations :

```typescript
import { Component, OnInit } from '@angular/core';
import { ApiService } from './api.service';
import { Location } from './models';

@Component({
  selector: 'app-locations',
  templateUrl: './locations.component.html',
  styleUrls: ['./locations.component.css']
})
export class LocationsComponent implements OnInit {
  locations: Location[];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.apiService.getLocations().subscribe((data: Location[]) => {
      this.locations = data;
    });
  }
}
```

Exemple de composant pour créer une location avec un formulaire :

```typescript
import { Component } from '@angular/core';
import { ApiService } from './api.service';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-create-location',
  templateUrl: './create-location.component.html',
  styleUrls: ['./create-location.component.css']
})
export class CreateLocationComponent {
  locationForm: FormGroup;

  constructor(private apiService: ApiService, private fb: FormBuilder) {
    this.locationForm = this.fb.group({
      name: [''],
      description: [''],
      availableFrom: [''],
      availableTo: [''],
      maxGuests: [''],
      includesTransport: [''],
      price: [''],
      city: [''],
      image: [null]
    });
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.locationForm.patchValue({
        image: file
      });
    }
  }

  submit() {
    const formData = new FormData();
    Object.keys(this.locationForm.controls).forEach(key => {
      formData.append(key, this.locationForm.get(key).value);
    });

    this.apiService.createLocation(formData).subscribe(response => {
      console.log('Location created', response);
    });
  }
}
```

### Modèles utilisés côté front

Créez un fichier `models.ts` pour définir les interfaces des données utilisées :

```typescript
export interface User {
  id?: number;
  username: string;
  email: string;
  password?: string;
}

export interface Location {
  id?: number;
  name: string;
  description: string;
  availableFrom: Date;
  availableTo: Date;
  maxGuests: number;
  includesTransport: boolean;
  price: number;
  city: string;
  imagePath?: string;
  image?: File;
}

export interface Reservation {
  id?: number;
  startDate: Date;
  endDate: Date;
  numberOfGuests: number;
  totalPrice: number;
  userId: number;
  locationId: number;
  status?: string;
  flights?: Flight[];
  hotels?: Hotel[];
}

export interface Flight {
  id?: number;
  flightNumber: string;
  departureAirport: string;
  arrivalAirport: string;
  departureTime: Date;
  arrivalTime: Date;
  price: number;
  airline: string;
  reservationId: number;
  locationId: number;
}

export interface Hotel {
  id?: number;
  name: string;
  location: string;
  pricePerNight: number;
  reservationId: number;
}

export interface Payment {
  token: string;
  amount: number;
  currency: string;
  reservationId: number;
}
```