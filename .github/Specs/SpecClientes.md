# Especificación Técnica: Componente CRUD de Clientes

**Estado:** Borrador / Listo para Revisión  
**Versión:** 1.0.0  
**Fecha:** 01 de Octubre de 2026  
**Metodología:** Spec-Driven Development (SDD)

\---

## 1\. Introducción y Objetivo

El objetivo de este componente es proporcionar una interfaz unificada, segura y reutilizable para la gestión del ciclo de vida de los clientes (CRUD: Crear, Leer, Actualizar y Eliminar) dentro del ecosistema del sistema.
El cliente debe tener asignado un tipo de cliente y puede tener muchos intereses de compra.

## 2\. Modelo de Datos (Esquema)

El componente manejará la entidad `Cliente` con las siguientes propiedades estrictas:

|Campo|Tipo de Datos|Restricciones / Validaciones|Descripción|
|-|-|-|-|
|`Id`|UUID v4|Requerido, Único, Autogenerado|Identificador único del cliente.|
|`Nombre`|String|Requerido, Min: 2, Max: 80 caracteres.|Nombre completo del cliente.|
|`Correo`|String|Requerido, Único, Formato de Email válido (RFC 5322).|Correo electrónico de inicio de sesión y contacto.|

## 3\. Interfaces del Servicio

### 3.1. Crear Cliente (Create)

* **Método/Función:** `CrearCliente(data: ClienteInput) -> Void`
* **Salida Exitosa:** Agregado correctamente.
* **Errores Posibles:** `Correo repetido` si el correo ya se encuentra registrado.

### 3.2. Obtener Cliente por ID (Read)

* **Método/Función:** `GetClienteById(id: UUID) -> ClienteResponse`
* **Salida Exitosa:** Detalle del cliente.
* **Errores Posibles:** `404 Not Found` si el ID no existe.

### 3.3. Obtener todos los clientes (Read)

* **Método/Función:** `GetAllClientes() -> List<ClienteResponse>`
* **Salida Exitosa:** Todos los clientes registrados.
* 
### 3.3. Actualizar Cliente (Update)

* **Método/Función:** `UpdateCliente(id: UUID, data: ClienteInput) -> Void`
* **Entrada:** (Todos los campos son opcionales pero deben cumplir las validaciones si se envían).
* **Errores Posibles:** `Correo repetido` si el correo ya se encuentra registrado.

### 3.4. Eliminar Cliente (Delete)

* **Método/Función:** `DeleteCliente(id: UUID) -> Void`
* **Salida Exitosa:** Sin cuerpo de respuesta.
* **Errores Posibles:** `404 Not Found` si el cliente no existe.

## 4\. Organización del Código y Estructura de Carpetas
Para los componentes de presentación, se recomienda la siguiente estructura de carpetas:
```
WebApp/
	Components/
		Clientes/
			ClienteForm.razor
			ClientesList.razor
```
El componente ClienteForm debe ser accesado por la URL `/Clientes/nuevo` para crear un cliente y `/Clientes/editar/{id:UUID}` para modificar un cliente.
El componente ClientesList debe ser accesado por la URL `/Clientes` para listar los clientes.
```

## 5\. Requisitos Funcionales

1. Todos los campos deben cumplir con las restricciones de longitud y formato especificadas en el modelo de datos.
2. El ClienteForm se utiliza para crear y actualizar cliente, validando los datos antes de enviarlos al backend. La validación deben ser con ValidationMessage por campo. Usar `EditForm` con validación integrada.
3. El ClientesList debe mostrar todos los clientes existentes, con opciones para agregar un nuevo cliente, editar o eliminar cada cliente de manera independiente. Se debe agregar paginación si la lista supera los 10 clientes. - Usar `QuickGrid` para tablas.
4. Cada cliente debe tener asignado un tipo de cliente.
5. Los tipos de cliente deben ser representados en un `select` desplegable, mostrando las descripciones legibles para el usuario final.
6. Un cliente puede tener muchos intereses de compra.

## 6\. Requisitos No Funcionales y Seguridad

1. **Idempotencia:** Las operaciones de lectura (`Read`) y eliminación (`Delete`) deben ser idempotentes.
2. **Manejo de Errores Estandarizado:** Cualquier fallo de validación (ej. formato de correo inválido o contraseña débil) debe retornar una estructura de error consistente:
3. **Interfas de usuario:** Utiliza estilos Bootstrap para formularios y listas, asegurando compatibilidad con dispositivos móviles y accesibilidad (WCAG 2.1).
4. **Botones de acción:** Los botones de acción (Crear, Editar, Eliminar) deben estar claramente etiquetados y deshabilitados cuando la acción no sea aplicable. Se debe utilizar iconos representativos para mejorar la experiencia del usuario.
5. **Apariencia de la lista de clientes:** Utilizar QuickGrid para mejorar la apariencia e implementar paginación cuando la lista sea mayor a 10 clientes. La lista de clientes debe mostrar columnas para `Nombre`, `Correo` y `Rol`. Se debe permitir ordenar por cada columna y filtrar por rol.

## 7\. Criterios de Aceptación (Comportamiento Esperado)

### Escenario 1: Creación exitosa de un cliente

* **GIVEN** que un administrador envía un nombre válido, un correo no registrado previamente y una contraseña que cumple los requisitos de complejidad.
* **WHEN** se invoca la función de creación de cliente.
* **THEN** el sistema debe almacenar el cliente con la contraseña cifrada, retornar un código de éxito.

### Escenario 2: Intento de registro con correo duplicado

* **GIVEN** que el correo `juan.perez@example.com` ya existe en la base de datos.
* **WHEN** se intenta crear un cliente con ese mismo correo.
* **THEN** el componente debe rechazar la solicitud de inmediato, lanzando una excepción o código de error `409 Conflict`, sin alterar los registros existentes.
