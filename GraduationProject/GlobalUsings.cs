global using System.Text;
global using System.Reflection;
global using System.Security.Claims;
global using System.IdentityModel.Tokens.Jwt;
global using System.ComponentModel.DataAnnotations;
global using System.Security.Cryptography;

global using Mapster;
global using MapsterMapper;

global using FluentValidation;
global using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.UI.Services;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Cors;
global using Microsoft.AspNetCore.WebUtilities;
global using Microsoft.Extensions.Options;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.IdentityModel.Tokens;

global using GraduationProject;
global using GraduationProject.Abstractions;
global using GraduationProject.Authentication;
global using GraduationProject.Abstractions.Consts;
global using GraduationProject.Contracts.Authentication;
global using GraduationProject.Contracts.User;
global using GraduationProject.Contracts.Question;
global using GraduationProject.Contracts.Lesson;
global using GraduationProject.Entities;
global using GraduationProject.Errors;
global using GraduationProject.Extensions;
global using GraduationProject.Entities.Enums;
global using GraduationProject.Services;
global using GraduationProject.Settings;
global using GraduationProject.Helpers;
global using GraduationProject.Persistence; 

global using MimeKit;
global using MailKit;
global using MailKit.Net.Smtp;
global using MailKit.Security;

global using Hangfire;
global using HangfireBasicAuthenticationFilter;



