# Especificación Técnica: Acceso y autenticación de usuarios

**Estado:** Borrador / Listo para Revisión  
**Versión:** 1.0.0  
**Fecha:** 10 de Julio de 2026  
**Metodología:** Spec-Driven Development (SDD)

\---

## 1\. Introducción y Objetivo
El objetivo de esta especificación es crear un componente de acceso, autentificación y autorización a la plataforma.

## 2\. Modelo de Datos (Esquema)
El componente manejará la entidad `Usuarios` para validar el acceso a la plataforma por medio del correo y el password, además de conocer el Rol del usuario que será tomado en cuenta para el acceso a la plataforma y restricciones en algunos componentes.

## 3\. Interfaces del Servicio
### 3.1. Login Usuario
* **Método/Función:** `LoginUsuario(data: LoginInput) -> UserResponse`
* **Salida Exitosa:** Usuario validado, autorizado y registrado por medio de una Cooky.
* **Errores Posibles:** `Correo no existente` si el correo del usuario no se encuentra registrado. `Clave de acceso incorrecta` si la clave no coincide con la registrada.

## 4\. Organización del Código y Estructura de Carpetas
Para los componentes de presentación, se recomienda la siguiente estructura de carpetas:
```
WebApi/
	Components/
		Auth/
			LoginForm.razor
```

## 5\. Requisitos Funcionales
1. Todos los campos deben cumplir con las restricciones de longitud y formato especificadas en el modelo de datos.
2. Se deben validar los datos antes de enviarlos al backend. La validación deben ser con ValidationMessage por campo.
3. La interfaz de usuario debe permitir al usuario ingresar su correo y contraseña, y proporcionar retroalimentación clara en caso de errores de validación o autenticación.
4. La interfaz debe redirigir al usuario a la página principal de la aplicación después de un inicio de sesión exitoso, y mostrar mensajes de error apropiados en caso de fallas.
5. La sesión del usuario debe mantenerse activa mientras el usuario esté interactuando con la aplicación, y debe expirar después de un período de inactividad definido.
6. La autenticación debe ser segura y mantener las credenciales del usuario utilizando cookies, asegurando que la información sensible no se exponga en el frontend.
7. La autorización debe basarse en el rol del usuario, permitiendo o restringiendo el acceso a diferentes componentes de la aplicación según el nivel de permisos asignado.
8. Las restricciones de acceso deben ser implementadas en el frontend y cumplir con:
8.1. Los usuarios con el rol de `Administrador` deben tener acceso completo a todas las funcionalidades de la aplicación.
8.2. Los usuarios con el rol de `Usuario` deben tener acceso a el listado de clientes y debe tener deshabilitadas las opciones de agregar, modificar y eliminar cliente.
9. No utilizar Identity ni ningún otro framework de autenticación externo. La autenticación debe ser implementada de manera personalizada dentro del sistema.

## 6\. Requisitos No Funcionales y Seguridad
3. **Manejo de Errores Estandarizado:** Cualquier fallo de validación debe retornar una estructura de error consistente.
4. **Interfas de usuario:** Utiliza estilos Bootstrap, asegurando compatibilidad con dispositivos móviles y accesibilidad (WCAG 2.1).
5. **Botones de acción:** Los botones de acción deben estar claramente etiquetados y deshabilitados cuando la acción no sea aplicable. Se debe utilizar iconos representativos para mejorar la experiencia del usuario.

## 7\. Criterios de Aceptación (Comportamiento Esperado)

### Escenario 1: Login exitoso
* **GIVEN** que un usuario escribe su correo y password validos y correctos.
* **WHEN** se invoca la funcionalidad de login de usuario.
* **THEN** el sistema debe generar autentificar al usuario y autorizar los accesos por medio de cooky.

### Escenario 2: Intento de acceso con correo no registrado
* **GIVEN** el correo `juan.perez@example.com` no se encuentra registrado como usuario.
* **WHEN** no es localizado en la tabla de usuarios.
* **THEN** el componente rechazar el acceso, lanzando una excepción o código de error `404 Not Found`.

### Escenario 3: Intento de acceso con clave incorrecta
* **GIVEN** se encuentra un usuario registrado con el correo `juan.perez@example.com` pero la clave ingresada es incorrecta.
* **WHEN** se invoca la funcionalidad de login de usuario.
* **THEN** el sistema debe rechazar el acceso, lanzando una excepción o código de error `401 Unauthorized`.

### Escenario 4: Intento de acceso con campos vacíos
* **GIVEN** que el usuario deja los campos de correo y/o password vacíos.
* **WHEN** se invoca la funcionalidad de login de usuario.
* **THEN** el sistema debe rechazar el acceso, mostrando un mensaje de error indicando que los campos son obligatorios y no pueden estar vacíos.

### Escenario 5: Intento de acceso con formato de correo inválido
* **GIVEN** que el usuario ingresa un correo con formato inválido (por ejemplo "usuario@dominio" sin el ".com").
* **WHEN** se invoca la funcionalidad de login de usuario.
* **THEN** el sistema debe rechazar el acceso, mostrando un mensaje de error indicando que el formato del correo es inválido y debe corregirse antes de continuar.

## 8\. Consideraciones Adicionales
* La implementación del componente de acceso y autenticación debe seguir las mejores prácticas de seguridad, incluyendo el uso de hashing seguro para las contraseñas y la protección contra ataques de fuerza bruta.
* La interfaz de usuario debe ser intuitiva y accesible, proporcionando una experiencia de usuario fluida y coherente con el resto de la plataforma.
* Restringir el acceso a las rutas de la aplicación según el rol del usuario, asegurando que los usuarios no autorizados no puedan acceder a funcionalidades o datos sensibles.
* Agregar lo necesario a los componentes de presentación para que el usuario pueda cerrar sesión de manera segura, invalidando la cookie de sesión y redirigiendo al usuario a la página de inicio de sesión.
* En el top-row del layout principal, se debe mostrar el nombre del usuario autenticado y su rol, proporcionando un enlace para cerrar sesión de manera segura.
