﻿global using MediatR;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Reservation.Application.Features.Users.Commands;
global using Reservation.Application.Features.Users.Commands.UserLogin;
global using Microsoft.AspNetCore.Authorization;
global using Reservation.Application.DTOs.UserDTOs;
global using Reservation.Application.Features.RoomReservations.Command.DeleteRoomReservation;
global using Reservation.Application.Features.RoomReservations.Command.UpdateReservations;
global using System.Security.Claims;
global using Reservation.Application.Features.RoomReservations.Command.CreateReservation;
global using Reservation.Application.Features.Rooms.Queries.GetRoomsAvailability;