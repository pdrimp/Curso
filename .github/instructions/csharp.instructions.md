---
description: Reglas de C# para proyectos Blazor y .NET
applyTo: "**/*.cs"
---

# Instrucciones para archivos C# de .NET y Blazor

- Usa C# moderno y convenciones estándar de .NET.
- Mantén las clases y métodos pequeños, cohesionados y con una responsabilidad clara.
- Usa nombres descriptivos para clases, métodos, variables y parámetros.
- Usa PascalCase para clases, métodos, propiedades, eventos, enumeraciones y constantes.
- Usa camelCase para variables locales y parámetros.
- Prefiere nombres que expresen intención; evita abreviaturas poco claras.
- Habilita y respeta tipos de referencia anulables cuando el proyecto los tenga configurados.
- Usa `var` cuando el tipo sea evidente por la expresión del lado derecho; usa el tipo explícito cuando mejore la legibilidad.
- Usa `async` y `await` para operaciones de entrada/salida.
- No uses `.Result`, `.Wait()` ni bloqueos síncronos sobre tareas asíncronas.
- Recibe y propaga `CancellationToken` en operaciones asíncronas de larga duración o que llamen a servicios externos.
- Valida los argumentos públicos y lanza excepciones claras cuando corresponda.
- No captures `Exception` sin manejarla de forma útil.
- No ignores excepciones silenciosamente.
- No uses excepciones para controlar el flujo normal de la aplicación.
- Prefiere inyección de dependencias por constructor para servicios requeridos.
- No instancies directamente servicios de infraestructura, repositorios, clientes HTTP ni contextos de base de datos dentro de componentes, controladores o clases de negocio.
- Define interfaces para dependencias que deban sustituirse, probarse o desacoplarse.
- Mantén la lógica de negocio fuera de los componentes Razor.
- Separa las responsabilidades de presentación, aplicación, dominio e infraestructura cuando la arquitectura del proyecto lo requiera.
- Evita métodos demasiado largos; extrae métodos privados con nombres que expliquen la intención.
- Evita código duplicado; reutiliza servicios, extensiones o métodos privados cuando tenga sentido.
- Usa LINQ de forma legible; evita consultas excesivamente complejas en una sola expresión.
- No expongas secretos, cadenas de conexión, tokens, contraseñas ni claves API en el código.
- Usa configuración mediante `appsettings.json`, User Secrets, variables de entorno o un proveedor seguro de secretos.
- Escribe código fácil de probar y evita dependencias estáticas innecesarias.
- Para servicios HTTP, usa `IHttpClientFactory` o el `HttpClient` configurado mediante inyección de dependencias.
- Añade comentarios solo cuando expliquen decisiones, restricciones o razones que el código no deja claras.
- No agregues dependencias NuGet sin explicar por qué son necesarias.
- Genera código compatible con la versión de .NET configurada en el archivo `.csproj`.