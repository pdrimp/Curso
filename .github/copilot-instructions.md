# Instrucciones para GitHub Copilot

## Propósito
Este repositorio contiene una aplicación Web desarrollada con .NET Blazor bajo principios de Clean Architecture.  
El objetivo es mantener el código mantenible, testeable, desacoplado y fácil de evolucionar.

## Objetivo de la aplicación
Mantener la gestión eficiente del catálogo de clientes de una empresa de ventas. A la empresa le interesa gestionar los datos básicos de los clientes: nombre completo, teléfono y correo electrónico. Además, es esencial asignar a cada cliente un tipo de cliente (por ejemplo, "VIP", "Regular", "Potencial" o "Inactivo") para priorizar estrategias de atención, y un conjunto de intereses de compra (como "electrónicos", "ropa", "servicios jurídicos", "tecnología" o "inmuebles") para personalizar ofertas y campañas de marketing.

## Reglas obligatorias

### Arquitectura
- Seguir Clean Architecture como principio base.
- Mantener las dependencias apuntando hacia el núcleo de la solución.
- La capa de UI no debe depender directamente de la infraestructura.
- La lógica de negocio no debe vivir en la UI.
- La lógica de acceso a datos no debe mezclarse con la lógica de negocio.
- Aplicar el patron de servicios.
- Aplicar el patron de repositorios.

### UI y Blazor
- Usar Blazor como capa de presentación.
- Todo el flujo funcional debe resolverse mediante componentes, servicios y repositorios.
- Los componentes Blazor deben ser delgados.
- Los componentes no deben contener lógica de negocio compleja.
- Los componentes solo deben orquestar la interacción con servicios de aplicación.
- Los componentes solo deben cumplir con un objetivo funcional específico.
- Todas las validaciones de datos deben realizarse con DataAnnotations o FluentValidation en la capa de aplicación.
- Usar `EditForm` con validación integrada y con dato en el atributo FormName.

### Servicios
- Toda operación de negocio debe exponer un servicio de aplicación.
- Los servicios deben contener la lógica de caso de uso.
- Los servicios no deben acceder al `DbContext` directamente.
- Los servicios deben depender de abstracciones, no de implementaciones concretas.
- Los servicios deben ser pequeños, cohesivos y con una responsabilidad clara.

### Repositorios
- Usar el patrón Repository para encapsular la persistencia.
- Los repositorios son la única vía para acceder a datos desde los servicios.
- Los repositorios no deben contener lógica de negocio.
- Los repositorios no deben devolver entidades manipuladas de forma insegura.
- Evitar exponer `IQueryable` fuera del repositorio.
- Cada repositorio debe representar una unidad coherente de acceso a datos.

### Persistencia
- Debe mantenerse en la capa de infraestructura.
- Usar Entity Framework Core con enfoque Code First con conexión a SQL Server local.
- Usar `DbContext` para SQL Server.
- Configurar la persistencia mediante Fluent API cuando sea posible.
- Mantener las entidades limpias, sin contaminación de detalles de infraestructura, ni datannotations.
- Las migraciones deben generarse desde el modelo Code First.
- El acceso al `DbContext` debe estar encapsulado en la capa de infraestructura.

### Dependencia e inyección
- Usar Dependency Injection cuando sea necesario para desacoplar componentes.
- Registrar interfaces y sus implementaciones en el composition root.
- No instanciar manualmente servicios o repositorios cuando exista DI disponible.
- `DbContext` debe registrarse con ciclo de vida adecuado para el uso web.
- Los repositorios y servicios deben respetar el ciclo de vida definido por DI.

### Diseño y buenas prácticas
- Priorizar separación de responsabilidades.
- Evitar clases grandes y acopladas.
- Preferir composición sobre herencia cuando sea posible.
- Mantener nombres claros, explícitos y consistentes.
- Aplicar validación en la capa apropiada.
- Usar async/await en operaciones I/O.
- Mantener el código legible antes que “ingenioso”.
- Evitar duplicación de reglas y lógica.
- Agregar comentarios solo cuando el código no sea auto explicativo.
- Todos los mensajes de error deben ser claros y amigables para el usuario final.

## Estructura recomendada
- `WebApp/` para Blazor UI.
- `Application/` para servicios, contratos y casos de uso.
- `Application/DTOs` para colocar las clases DTO.
- `Application/Interfaces` para las interfases de servicios y repositorios.
- `Application/Services` para implementar los ervicios.
- `Domain/Entities` para entidades y reglas del negocio.
- `Infrastructure/Data` para crear y mantener el DbContext.
- `Infrastructure/Repositories` para implementar los repositorios.
- `.github/Specs/` para documentar las especificaciones.
- `.github/Plans/` para guardar los archivos de planes generados.


## Reglas para entidades
- Las entidades deben modelar el dominio, no la base de datos.
- No exponer setters públicos innecesarios.
- Proteger invariantes del dominio.
- Usar métodos de dominio para modificar estado cuando aplique.
- No utilizar DataAnnotations.

## Reglas para DTOs
- Usar DTOs para entrada y salida entre UI y servicios.
- No exponer entidades EF directamente a la UI cuando exista una alternativa adecuada.
- Mapear entre DTOs y entidades en la capa adecuada.
- Usar AutoMapper o mapeo manual según la complejidad del proyecto.
- Mantener DTOs simples y enfocados en la operación específica.
- Evitar lógica de negocio en DTOs.

## Reglas para EF Core
- Configurar relaciones, índices y restricciones con Fluent API.
- Mantener configuraciones de entidad separadas cuando el modelo crezca.
- No colocar lógica de negocio dentro del `DbContext`.
- Usar `DbSet<T>` solo para entidades persistentes del dominio.
- Mantener las migraciones revisadas y consistentes.

## Reglas de calidad
- Todo código nuevo debe ser fácil de probar.
- Preferir pruebas unitarias para servicios y reglas de negocio.
- Evitar dependencias innecesarias en infraestructura durante pruebas.
- Cualquier cambio debe respetar el diseño existente antes de introducir nuevas abstractions.

## Prohibiciones
- No acceder a la base de datos desde componentes Blazor.
- No usar `DbContext` directamente en la UI.
- No mezclar negocio con persistencia.
- No crear servicios “Dios” con demasiadas responsabilidades.
- No acoplar la solución a SQL Server en capas que deberían ser agnósticas.
- No usar estáticos globales para resolver dependencias.

## Convenciones de trabajo
- Antes de crear una clase nueva, validar si ya existe un servicio o repositorio reutilizable.
- Antes de agregar una dependencia, evaluar si puede resolverse por interfaz.
- Antes de mover código, respetar la dirección de dependencias de Clean Architecture.
- Antes de exponer un método público, confirmar que pertenece a la responsabilidad de la clase.
- Mantener archivos separados.
- Todos los documentos generados por IA deben colocarse en la carpeta de la solución `.github/Plans/`.

## Criterio de aceptación
Una implementación es aceptable solo si:
- respeta Clean Architecture,
- no usa controladores,
- usa servicios y repositorios,
- usa EF Core Code First con SQL Server,
- mantiene la UI desacoplada de persistencia,
- y conserva un diseño simple, consistente y testeable.