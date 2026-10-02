# Especificación Técnica: Componente CRUD de Usuarios

**Estado:** Borrador / Listo para Revisión  
**Versión:** 1.0.0  
**Fecha:** 23 de Junio de 2026  
**Metodología:** Spec-Driven Development (SDD)

\---

## 1\. Introducción y Objetivo

El objetivo de este componente es proporcionar una interfaz unificada, segura y reutilizable para la gestión del ciclo de vida de los usuarios de la plataforma (CRUD: Crear, Leer, Actualizar y Eliminar).

## 2\. Modelo de Datos (Esquema)

El componente manejará la entidad `Users` con las siguientes propiedades estrictas:

|Campo|Tipo de Datos|Restricciones / Validaciones|Descripción|
|-|-|-|-|
|`Id`|UUID v4|Requerido, Único, Autogenerado|Identificador único del usuario.|
|`Nombre`|String|Requerido, Min: 2, Max: 80 caracteres.|Nombre completo del usuario.|
|`Correo`|String|Requerido, Único, Formato de Email válido (RFC 5322).|Correo electrónico de inicio de sesión y contacto.|
|`Password`|String|Requerido, Min: 8 caracteres. Debe contener 1 mayúscula, 1 minúscula, 1 número y 1 carácter especial.|Contraseña del usuario (Almacenada con hashing seguro bcrypt/Argon2).|
|`Rol`|String|Requerido, Debe ser: Administrador, Usuario.| Es un campo que define el nivel de acceso y permisos del usuario dentro del sistema.|

## 3\. Interfaces del Servicio

### 3.1. Crear Usuario (Create)

* **Método/Función:** `CreateUser(data: UserInput) -> UserResponse`
* **Salida Exitosa:** Agregado correctamente.
* **Errores Posibles:** `Correo repetido` si el correo ya se encuentra registrado.

### 3.2. Obtener Usuario por ID (Read)

* **Método/Función:** `GetUserById(id: UUID) -> UserResponse`
* **Salida Exitosa:** Detalle del usuario sin el campo `password`.
* **Errores Posibles:** `404 Not Found` si el ID no existe.

### 3.3. Obtener todos los usuarios (Read)

* **Método/Función:** `GetAllUsers() -> List<UserResponse>`
* **Salida Exitosa:** Todos los usuarios registrados.
* 
### 3.3. Actualizar Usuario (Update)

* **Método/Función:** `UpdateUser(id: UUID, data: UserInput) -> UserResponse`
* **Entrada:** (Todos los campos son opcionales pero deben cumplir las validaciones si se envían).
* **Salida Exitosa:** Objeto de usuario actualizado.

### 3.4. Eliminar Usuario (Delete)

* **Método/Función:** `DeleteUser(id: UUID) -> Void`
* **Salida Exitosa:** Sin cuerpo de respuesta.
* **Errores Posibles:** `404 Not Found` si el usuario no existe.

## 4\. Organización del Código y Estructura de Carpetas
Para los componentes de presentación, se recomienda la siguiente estructura de carpetas:
```
WebApp/
	Components/
		Usuarios/
			UsuarioForm.razor
			UsuariosList.razor
```
Para los enumerados de usuarios, se recomienda la siguiente estructura:
```
Domain/
	Enums/
		UserRoles.cs
```
El componente UsuarioForm debe ser accesado por la URL `/usuario/nuevo` para crear un usuario y `/usuario/editar/{id:UUID}` para modificar un usuario.
El componente UsuariosList debe ser accesado por la URL `/usuarios` para listar los usuarios.
```

## 5\. Requisitos Funcionales

1. Todos los campos deben cumplir con las restricciones de longitud y formato especificadas en el modelo de datos.
2. El UsuarioForm se utiliza para crear y actualizar usuario, validando los datos antes de enviarlos al backend. La validación deben ser con ValidationMessage por campo.
3. El UsuariosList debe mostrar todos los usuarios existentes, con opciones para agregar un nuevo usuario, editar o eliminar cada usuario de manera independiente. Se debe agregar paginación si la lista supera los 10 usuarios.
4. Los enums de usuarios deben ser representados en un `select` desplegable, mostrando los nombres legibles para el usuario final.

## 6\. Requisitos No Funcionales y Seguridad

1. **Seguridad de Contraseñas:** El componente no debe almacenar el `password` en texto plano bajo ninguna circunstancia. Debe aplicar un algoritmo de hashing robusto (mínimo BCrypt con factor de costo de 10 o Argon2id) antes de persistir en la base de datos.
2. **Idempotencia:** Las operaciones de lectura (`Read`) y eliminación (`Delete`) deben ser idempotentes.
3. **Manejo de Errores Estandarizado:** Cualquier fallo de validación (ej. formato de correo inválido o contraseña débil) debe retornar una estructura de error consistente:
4. **Interfas de usuario:** Utiliza estilos Bootstrap para formularios y listas, asegurando compatibilidad con dispositivos móviles y accesibilidad (WCAG 2.1).
5. **Botones de acción:** Los botones de acción (Crear, Editar, Eliminar) deben estar claramente etiquetados y deshabilitados cuando la acción no sea aplicable. Se debe utilizar iconos representativos para mejorar la experiencia del usuario.
6. **Apariencia de la lista de usuarios:** Utilizar QuickGrid para mejorar la apariencia e implementar paginación cuando la lista sea mayor a 10 usuarios. La lista de usuarios debe mostrar columnas para `Nombre`, `Correo` y `Rol`. Se debe permitir ordenar por cada columna y filtrar por rol.

## 7\. Criterios de Aceptación (Comportamiento Esperado)

### Escenario 1: Creación exitosa de un usuario

* **GIVEN** que un administrador envía un nombre válido, un correo no registrado previamente y una contraseña que cumple los requisitos de complejidad.
* **WHEN** se invoca la función de creación de usuario.
* **THEN** el sistema debe almacenar el usuario con la contraseña cifrada, retornar un código de éxito.

### Escenario 2: Intento de registro con correo duplicado

* **GIVEN** que el correo `juan.perez@example.com` ya existe en la base de datos.
* **WHEN** se intenta crear un usuario con ese mismo correo.
* **THEN** el componente debe rechazar la solicitud de inmediato, lanzando una excepción o código de error `409 Conflict`, sin alterar los registros existentes.

### Escenario 3: Validación de complejidad de contraseña

* **GIVEN** una contraseña que no cumple los requisitos (por ejemplo, `"123"`).
* **WHEN** se intenta registrar o actualizar el usuario.
* **THEN** el sistema debe impedir la persistencia y devolver un error `400 Bad Request` indicando la regla de complejidad incumplida.
