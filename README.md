# International Business Men APP

Aplicación creada para ayudar a los ejecutivos de una empresa X que vuelan por todo el mundo. Los ejecutivos de dicha empresa necesitan un listado de cada producto con el cual comercian, y el total de la suma de las ventas de estos productos.

Para ofrecer la solución a dicho requerimiento se creó una página web SPA y webservice REST. Este webservice devuelve los resultados en formato JSON.

## Especificaciones del webservice creado

El webservice desarrollado para cumplir con los requisitos previamente planteados tiene los siguientes métodos disponibles:

- Un método que permite obtener un listado de todas las transacciones disponibles.
- Un método que permite obtener las transacciones de un producto específico.
- Un método que permite obtener una lista de todos los rates de conversión entre las diferentes monedas.

Además, se ha agregado soporte a swagger para facilitar las pruebas con el webservice creado. para acceder al recurso se debe utilizar el endpoint /swagger desde la ruta raíz del servicio publicado. Para el caso del ambiente de desarrollo por defecto la URL será:

[https://localhost:44347/swagger](https://localhost:44347/swagger)

Dicho recurso muestra la siguiente interfaz:

![Swagger definition file](/assets/service_swagger_definition.PNG)

## Especificaciones de la aplicación web creada

La aplicación web desarrollada contiene 3 vistas principales ofrecidas a los usuarios. La URL de inicio por defecto de la aplicación web es [https://localhost:44390](https://localhost:44390). La apariencia y descripción de cada una de estas vistas es la siguiente:

### Página principal

La apareciencia de la página principal es la siguiente:

![Home Page](/assets/web_home_page.png)

En esta página se muestra un cálido mensaje de bienvenida a los usuarios. La página principal es la página mostrada por defecto al iniciar la aplicación por primera vez.

### Página de conversiones (rates)

La apariencia de la página de rates es la siguiente:

![Rates Page](/assets/web_rates_page.png)

En esta página se muestra la tabla de conversiones (rates) entre las distintas monedas manejadas por el servicio fuente de información para la aplicación. Las conversiones faltantes se calculan en background y se utilizan de forma transparente al usuario al mostrar las transacciones.

### Página de transacciones

La apariencia de la página de transacciones es la siguiente:

![Transactions Page](/assets/web_transactions_page.png)

En esta página se muestra la lista de las transacciones (ventas) de todos los productos obtenidas desde el proveedor de datos disponible para la aplicación. Se incluye una sección de búsqueda por SKU de cada producto y una opción de limpiado para eliminar el criterio de búsqueda. Además, se provee un control para cambiar la moneda utilizada al momento de mostrar los montos convertidos, el cual por defecto los muestra en Euros. Por último, se provee un control simple de paginación de la información, la cual muestra inicialmente muestra 10 transacciones por página.

# Arquitectura de la solución

La solución ofrecida para el requerimiento se divide en 2 partes principales:

- Un servicio web RESTFUL desarrollado en ASP.NET CORE 3.1 que contiene una capa de persistencia de emergencia utilizando Entity Framework Core 3.1 como ORM para conectar a una instancia de base de datos local SQL Server Express (LocalDb).
- Una aplicación web SPA desarrollada con REACT como framework principal para modelar la capa de presentación y conectar la información proveniente del servicio utilizando el apoyo de otras librerías/frameworks/lenguajes como Typescript, Redux, Thunk, React-Router, Bootstrap, entre otros.
  La arquitectura de cada uno de los componentes principales previamente señalados es la siguiente:

## Arquitectura del webservice

La vista a nivel de componentes del webservice desarrollado es la siguiente:

![Webservice Components](/assets/main_components_backend.PNG)

# Instrucciones de Instalación
