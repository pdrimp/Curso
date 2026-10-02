---
description: Reglas para componentes Razor y páginas Blazor
applyTo: "**/*.razor"
---

# Instrucciones para archivos Razor de Blazor

- Usa componentes Razor pequeños, legibles y con una responsabilidad clara.
- Prefiere HTML semántico y accesible: usa `main`, `nav`, `section`, `header`, `footer`, `button`, `label` y otros elementos apropiados.
- Incluye etiquetas `label` asociadas con los controles de formulario cuando aplique.
- No uses elementos clicables no semánticos, como `div` o `span`, en lugar de un elemento `button`.
- Usa `@inject` solo para servicios necesarios directamente en el componente.
- No coloques lógica de negocio, acceso a datos ni consultas a base de datos directamente en el marcado Razor.
- Para lógica extensa, crea o utiliza un archivo code-behind con el mismo nombre y extensión `.razor.cs`.
- Mantén los bloques `@code` cortos y enfocados en el estado y comportamiento de presentación.
- Usa `EventCallback` o `EventCallback<T>` para eventos que el componente notifica a su componente padre.
- Declara los parámetros públicos con `[Parameter]`.
- Declara los parámetros requeridos con `[EditorRequired]` cuando corresponda.
- Usa `@bind` para enlace bidireccional cuando sea apropiado.
- En formularios, usa `EditForm`, `EditContext` y validación con Data Annotations cuando sea adecuado.
- Muestra errores de validación con `ValidationMessage` o `ValidationSummary`.
- Usa `async` y `await` para operaciones asíncronas; evita `.Result` y `.Wait()`.
- Implementa `OnInitializedAsync`, `OnParametersSetAsync` u otros métodos de ciclo de vida asíncronos cuando deban realizar trabajo asíncrono.
- Evita llamadas repetidas e innecesarias a servicios durante cada renderizado.
- Usa `@key` en listas dinámicas cuando ayude a conservar correctamente la identidad de los elementos.
- No uses JavaScript interop si una característica nativa de Blazor o HTML resuelve el caso.
- Si se usa `IJSRuntime`, encapsula la interoperabilidad JavaScript en un servicio reutilizable cuando la lógica no sea trivial.
- No agregues paquetes, dependencias o bibliotecas externas sin justificar su necesidad.
- Genera código compatible con la versión de .NET definida en el archivo del proyecto.