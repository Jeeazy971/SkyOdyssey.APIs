# Documentation de l'API SkyOdyssey

## Introduction
SkyOdyssey est une API de r�servation de voyages qui permet aux utilisateurs de rechercher des h�tels, de r�server des vols et des h�tels, et de g�rer leurs r�servations. Cette documentation couvre tous les endpoints disponibles dans l'API et comment les utiliser c�t� front avec Angular/Ionic.

## Configuration de l'API

### CORS
Pour permettre � l'API d'accepter des requ�tes provenant de n'importe quelle origine, assurez-vous que le middleware CORS est correctement configur� dans `Program.cs`.

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

**Exemple de requ�te :**
```json
{
  "username": "john_doe",
  "email": "john@example.com",
  "password": "password123"
}
```

**R�ponse :**
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

**Exemple de requ�te :**
```json
{
  "username": "john_doe",
  "password": "password123"
}
```

**R�ponse :**
```json
{
  "token": "jwt-token-string"
}
```

#### R�cup�rer tous les utilisateurs
```
GET /api/users
```
**Description :** R�cup�re la liste de tous les utilisateurs.

**R�ponse :**
```json
[
  {
    "id": 1,
    "username": "john_doe",
    "email": "john@example.com"
  }
]
```

#### R�cup�rer un utilisateur par ID
```
GET /api/users/{id}
```
**Description :** R�cup�re un utilisateur par son ID.

**R�ponse :**
```json
{
  "id": 1,
  "username": "john_doe",
  "email": "john@example.com"
}
```

### Locations (H�tels)

#### R�cup�rer toutes les locations
```
GET /api/locations
```
**Description :** R�cup�re la liste de toutes les locations.

**R�ponse :**
```json
[
  {
    "id": 1,
    "name": "Paris Hotel",
    "description": "Profitez de notre h�tel Paris Hotel situ� au c�ur de Paris...",
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

#### R�cup�rer une location par ID
```
GET /api/locations/{id}
```
**Description :** R�cup�re une location par son ID.

**R�ponse :**
```json
{
  "id": 1,
  "name": "Paris Hotel",
  "description": "Profitez de notre h�tel Paris Hotel situ� au c�ur de Paris...",
  "availableFrom": "2023-01-01T00:00:00",
  "availableTo": "2023-12-31T23:59:59",
  "maxGuests": 4,
  "includesTransport": true,
  "price": 150.00,
  "city": "Paris",
  "imagePath": "uploads/abc123.jpg"
}
```

#### Cr�er une location
```
POST /api/locations
```
**Description :** Cr�e une nouvelle location.

**Exemple de requ�te :**
```json
{
  "name": "Paris Hotel",
  "description": "Profitez de notre h�tel Paris Hotel situ� au c�ur de Paris...",
  "availableFrom": "2023-01-01T00:00:00",
  "availableTo": "2023-12-31T23:59:59",
  "maxGuests": 4,
  "includesTransport": true,
  "price": 150.00,
  "city": "Paris",
  "image": "image-file"
}
```

**R�ponse :**
```json
{
  "id": 1,
  "name": "Paris Hotel",
  "description": "Profitez de notre h�tel Paris Hotel situ� au c�ur de Paris...",
  "availableFrom": "2023-01-01T00:00:00",
  "availableTo": "2023-12-31T23:59:59",
  "maxGuests": 4,
  "includesTransport": true,
  "price": 150.00,
  "city": "Paris",
  "imagePath": "uploads/abc123.jpg"
}
```

#### Mettre � jour une location
```
PUT /api/locations/{id}
```
**Description :** Met � jour une location existante.

**Exemple de requ�te :**
```json
{
  "name": "Updated Paris Hotel",
  "description": "Profitez de notre h�tel Paris Hotel situ� au c�ur de Paris...",
  "availableFrom": "2023-01-01T00:00:00",
  "availableTo": "2023-12-31T23:59:59",
  "maxGuests": 4,
  "includesTransport": true,
  "price": 150.00,
  "city": "Paris",
  "imagePath": "uploads/abc123.jpg"
}
```

**R�ponse :**
```json
{}
```

#### Supprimer une location
```
DELETE /api/locations/{id}
```
**Description :** Supprime une location par son ID.

**R�ponse :**
```json
{}
```

#### Rechercher des locations
```
GET /api/locations/search
```
**Description :** Recherche des locations bas�es sur diff�rents crit�res.

**Exemple de requ�te :**
```
GET /api/locations/search?searchTerm=Paris&availableFrom=2023-01-01&availableTo=2023-12-31&maxPrice=200&maxGuests=4
```

**R�ponse :**
```json
[
  {
    "id": 1,
    "name": "Paris Hotel",
    "description": "Profitez de notre h�tel Paris Hotel situ� au c�ur de Paris...",
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

### R�servations

#### R�cup�rer toutes les r�servations
```
GET /api/reservations
```
**Description :** R�cup�re la liste de toutes les r�servations.

**R�ponse :**
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

#### R�cup�rer une r�servation par ID
```
GET /api/reservations/{id}
```
**Description :** R�cup�re une r�servation par son ID.

**R�ponse :**
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

#### Cr�er une r�servation
```
POST /api/reservations
```
**Description :** Cr�e une nouvelle r�servation.

**Exemple de requ�te :**
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

**R�ponse :**
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

#### Mettre � jour une r�servation
```
PUT /api/reservations/{id}
```
**Description :** Met � jour une r�servation existante.

**Exemple de requ�te :**
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

**R�ponse :**
```json
{}
```

#### Supprimer une r�servation
```
DELETE /api/reservations/{id}
```
**Description :** Supprime une r�servation par son ID.

**R�ponse :**
```json
{}
```

### Vols

#### R�cup�rer tous les vols
```
GET /api/flights
```
**Description :** R�cup�re la liste de tous les vols.

**R�ponse :**
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

#### R�cup�rer un vol par ID
```
GET /api/flights/{id}
```
**Description :** R�cup�re un vol par son ID.

**R�ponse :**
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

#### Cr�er un vol
```
POST /api/flights
```
**Description :** Cr�e un nouveau vol.

**Exemple de requ�te :**
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

**R�ponse :**
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

#### Mettre � jour un vol
```
PUT /api/flights/{id}
```
**Description :** Met � jour un vol existant.

**Exemple de requ�te :**
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

**R�ponse :**
```json
{}
```

#### Supprimer un vol
```
DELETE /api/flights/{id}
```
**Description :** Supprime un vol par son ID.

**R�ponse :**
```json
{}
```

#### R�cup�rer les vols par location
```
GET /api/flights/by-location/{locationId}
```
**Description :** R�cup�re les vols disponibles pour une location donn�e.

**R�ponse :**
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

### H�tels

#### R�cup�rer tous les h�tels
```
GET /api/hotels
```
**Description :** R�cup�re la liste de tous les h�tels.

**R�ponse :**
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

#### R�cup�rer un h�tel par ID
```
GET /api/hotels/{id}
```
**Description :** R�cup�re un h�tel par son ID.

**R�ponse :**
```json
{
  "id": 1,
  "name": "Hotel Paris",
  "location": "Paris",
  "pricePerNight": 100.00,
  "reservationId": 1
}
```

#### Cr�er un h�tel
```
POST /api/hotels
```
**Description :** Cr�e un nouvel h�tel.

**Exemple de requ�te :**
```json
{
  "name": "Hotel Paris",
  "location": "Paris",
  "pricePerNight": 100.00,
  "reservationId": 1
}
```

**R�ponse :**
```json
{
  "id": 1,
  "name": "Hotel Paris",
  "location": "Paris",
  "pricePerNight": 100.00,
  "reservationId": 1
}
```

#### Mettre � jour un h�tel
```
PUT /api/hotels/{id}
```
**Description :** Met � jour un h�tel existant.

**Exemple de requ�te :**
```json
{
  "name": "Updated Hotel Paris",
  "location": "Paris",
  "pricePerNight": 120.00,
  "reservationId": 1
}
```

**R�ponse :**
```json
{}
```

#### Supprimer un h�tel
```
DELETE /api/hotels/{id}
```
**Description :** Supprime un h�tel par son ID.

**R�ponse :**
```json
{}
```

### Paiements

#### Effectuer un paiement
```
POST /api/payments/{reservationId}/pay
```
**Description :** Effectue un paiement pour une r�servation donn�e.

**Exemple de requ�te :**
```json
{
  "token": "tok_visa",
  "amount": 500,
  "currency": "eur",
  "reservationId": 1
}
```

**R�ponse :**
```json
{
  "status": "succeeded",
  "paymentIntentId": "pi_1234567890"
}
```

## Int�gration avec Angular/Ionic

### Configuration du service HTTP dans Angular

Cr�ez un service Angular pour interagir avec l'API.

```typescript
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User, Location, Reservation, Flight, Hotel, Payment } from './models'; // Importez les mod�les appropri�s

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

  // R�servations
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

  // H�tels
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

Exemple de composant pour cr�er une location avec un formulaire :

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

### Mod�les utilis�s c�t� front

Cr�ez un fichier `models.ts` pour d�finir les interfaces des donn�es utilis�es :

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