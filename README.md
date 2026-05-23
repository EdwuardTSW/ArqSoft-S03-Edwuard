# Catálogo de Motocicletas 2026

Aplicación web desarrollada como práctica académica universitaria para presentar un catálogo digital de motocicletas. El sistema permite consultar modelos disponibles, filtrar por marca y categoría, revisar información técnica detallada, registrar usuarios, iniciar sesión, publicar reseñas y administrar motocicletas mediante un super usuario.

## Propósito del Proyecto

El propósito de este proyecto es aplicar conceptos de desarrollo web con ASP.NET Core MVC, Razor Views, controladores, modelos, servicios, repositorios, estilos personalizados, autenticación por cookies y persistencia temporal mediante archivos JSON.

La aplicación simula el funcionamiento de un catálogo para una agencia distribuidora de motos, manteniendo un enfoque educativo y práctico.

## Descripción General

El sistema presenta una página principal con información introductoria, una sección de catálogo con motocicletas organizadas en tarjetas visuales, filtros de búsqueda, una vista de detalle para cada modelo, reseñas de usuarios y un formulario para agregar nuevas motocicletas.

La aplicación utiliza una interfaz responsive con estilo oscuro premium, componentes visuales personalizados, efectos tipo cristal, animaciones suaves y navegación mediante el patrón MVC.

## Funcionalidades Principales

- Página de inicio con presentación del catálogo.
- Listado de motocicletas disponibles.
- Búsqueda por marca, modelo o categoría.
- Filtro de motocicletas por marca y categoría.
- Ordenamiento por precio y año.
- Visualización de detalles técnicos por motocicleta.
- Registro e inicio de sesión de usuarios.
- Cierre de sesión.
- Publicación de reseñas con calificación de 1 a 5 estrellas.
- Restricción de una reseña por usuario por motocicleta.
- Promedio de calificaciones por motocicleta.
- Super usuario administrador creado automáticamente.
- Acciones administrativas protegidas por rol `Admin`.
- Formulario para agregar nuevas motocicletas solo para administrador.
- Eliminación de motocicletas solo para administrador.
- Política de privacidad personalizada.
- Diseño responsive adaptable a escritorio y dispositivos móviles.

## Capturas de Pantalla

Las siguientes secciones están reservadas para agregar capturas de pantalla del funcionamiento de la aplicación.

### Página de Inicio

<img width="1919" height="1037" alt="image" src="https://github.com/user-attachments/assets/b55e0ae5-9ff2-455f-8f97-896e6635c168" />


### Catálogo de Motocicletas

<img width="1919" height="1038" alt="image" src="https://github.com/user-attachments/assets/af63e359-90f6-4904-a097-9e0de84bb06a" />


### Filtros del Catálogo

<img width="1919" height="1036" alt="image" src="https://github.com/user-attachments/assets/2117a400-51fc-4d5d-bf6a-4d5b817a279b" />


### Detalle de Motocicleta

<img width="1919" height="1037" alt="image" src="https://github.com/user-attachments/assets/9891627b-2c96-4d9b-b148-48aaf7409f82" />


### Login y Registro

<img width="1919" height="1037" alt="image" src="https://github.com/user-attachments/assets/9b6f8369-db38-4b27-947f-8936144d91b1" />

<img width="1919" height="956" alt="image" src="https://github.com/user-attachments/assets/bc981052-0711-48eb-8d50-e81a13b61046" />

<img width="1919" height="956" alt="image" src="https://github.com/user-attachments/assets/93a9027e-b908-4fca-921c-ebf84812ed4c" />

### Admin

<img width="598" height="432" alt="image" src="https://github.com/user-attachments/assets/29a8f0c8-c245-4362-90c8-9b2c63213732" />

<img width="1075" height="235" alt="image" src="https://github.com/user-attachments/assets/8c21192f-1412-4f0c-9417-4a3643196d63" />


### Formulario para Agregar Motocicleta

<img width="1919" height="1032" alt="image" src="https://github.com/user-attachments/assets/d0acb707-7bee-44d3-99cc-f0526119eb42" />


## Tecnologías Utilizadas

- C#
- .NET 10
- ASP.NET Core MVC
- Razor Views
- Autenticación por cookies
- Claims y roles
- Bootstrap
- jQuery
- CSS personalizado
- Google Fonts
- Persistencia en archivos JSON

## Arquitectura del Proyecto

El proyecto está organizado por capas:

- `Catalogo.Domain`: contiene modelos e interfaces del dominio.
- `Catalogo.Application`: contiene servicios de aplicación y reglas de negocio.
- `Catalogo.Infrasctructure`: contiene repositorios JSON para persistencia temporal.
- `Catalogo.Presentation`: contiene controladores, vistas, estilos, imágenes y archivos de datos.

## Estructura Principal

- `Catalogo.Domain/Models`: contiene modelos como `Item`, `Usuario` y `Review`.
- `Catalogo.Domain/Interfaces`: contiene contratos de repositorios.
- `Catalogo.Application/Services`: contiene servicios como `ItemService`, `UsuarioService` y `ReviewService`.
- `Catalogo.Infrasctructure/Repositories`: contiene repositorios JSON.
- `Catalogo.Presentation/Controllers`: contiene los controladores MVC.
- `Catalogo.Presentation/ViewModels`: contiene modelos de vista para formularios y pantallas.
- `Catalogo.Presentation/Views`: contiene las vistas Razor.
- `Catalogo.Presentation/Data`: contiene archivos JSON usados como persistencia temporal.
- `Catalogo.Presentation/wwwroot`: contiene CSS, JavaScript, imágenes y librerías frontend.
- `Catalogo.Presentation/Program.cs`: define servicios, autenticación, rutas, middleware y creación del admin inicial.

## Modelo Principal

El modelo principal del catálogo es `Item`, que representa una motocicleta dentro del sistema. Sus propiedades principales son:

- `Id`: identificador de la motocicleta.
- `Marca`: marca del fabricante.
- `Modelo`: nombre del modelo.
- `Ano`: año del modelo.
- `Cilindrada`: capacidad del motor.
- `Precio`: precio de referencia.
- `Categoria`: tipo de motocicleta.
- `ImagenUrl`: dirección de la imagen utilizada en el catálogo.
- `Potencia`: potencia del motor.
- `Transmision`: tipo o cantidad de velocidades.
- `Color`: color principal.
- `Descripcion`: descripción general del modelo.

## Modelos Adicionales

### Usuario

Representa a un usuario registrado en la aplicación.

- `Id`: identificador del usuario.
- `Nombre`: nombre visible del usuario.
- `Email`: correo electrónico usado para iniciar sesión.
- `PasswordHash`: contraseña protegida mediante hash PBKDF2.
- `Rol`: rol del usuario, por ejemplo `Usuario` o `Admin`.
- `CreatedAt`: fecha de creación del usuario.

### Review

Representa una reseña publicada por un usuario.

- `Id`: identificador de la reseña.
- `ItemId`: identificador de la motocicleta reseñada.
- `UsuarioId`: identificador del usuario que publicó la reseña.
- `UsuarioNombre`: nombre visible del usuario.
- `Calificacion`: calificación de 1 a 5 estrellas.
- `Comentario`: comentario de la reseña.
- `CreatedAt`: fecha de creación de la reseña.

## Rutas Principales

- `/`: muestra la página de inicio.
- `/Catalogo`: muestra el catálogo completo de motocicletas.
- `/Catalogo?marca=Yamaha`: muestra el catálogo filtrado por marca.
- `/Catalogo?categoria=Naked`: muestra el catálogo filtrado por categoría.
- `/Catalogo/Detalle/{id}`: muestra el detalle de una motocicleta específica.
- `/Catalogo/Agregar`: muestra el formulario para agregar una nueva motocicleta. Requiere rol `Admin`.
- `/Account/Login`: página de inicio de sesión.
- `/Account/Register`: página de registro de usuario.
- `/Account/AccessDenied`: página de acceso denegado.
- `/Home/Privacy`: política de privacidad.

## Persistencia Temporal

La aplicación utiliza archivos JSON como base de datos temporal:

- `Catalogo.Presentation/Data/items.json`: catálogo de motocicletas.
- `Catalogo.Presentation/Data/users.json`: usuarios registrados.
- `Catalogo.Presentation/Data/reviews.json`: reseñas publicadas.

Los datos se conservan mientras los archivos JSON no sean eliminados o limpiados manualmente.

## Requisitos para Ejecutar el Proyecto

- .NET 10 SDK instalado.
- Visual Studio, Visual Studio Code u otro editor compatible con proyectos .NET.
- Navegador web moderno.

## Ejecución del Proyecto

Desde la raíz del repositorio:

```bash
dotnet run --project Catalogo.Presentation
```

También se puede compilar con:

```bash
dotnet build
```

Luego abre en el navegador la URL mostrada por la consola, por ejemplo:

```text
https://localhost:7000
```

## Consideraciones del Proyecto

- Este proyecto tiene fines académicos.
- La persistencia se realiza mediante archivos JSON, no mediante una base de datos relacional.
- Las contraseñas no se almacenan en texto plano; se protegen con PBKDF2.
- El administrador inicial se crea automáticamente al iniciar la aplicación si no existe un usuario con rol `Admin`.
- Las acciones de agregar y eliminar motocicletas están protegidas por rol.
- El diseño visual utiliza imágenes locales en `wwwroot/img` para evitar depender de enlaces externos.

## Enfoque Académico

Este proyecto fue desarrollado como una práctica universitaria para reforzar conocimientos de programación web con ASP.NET Core MVC. Su objetivo principal es demostrar la integración entre controladores, modelos, vistas, servicios, repositorios, rutas, formularios, autenticación, roles, persistencia temporal y estilos personalizados.

---

Proyecto realizado por el alumno Edwuard Chay, Tecnológico de Software.
