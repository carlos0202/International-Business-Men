
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
- Una aplicación web SPA desarrollada con react como framework principal para modelar la capa de presentación y conectar la información proveniente del servicio utilizando el apoyo de otras librerías/frameworks/lenguajes como typescript, redux, redux-thunk, react-router, bootstrap, entre otros.

La arquitectura de cada uno de los componentes principales previamente señalados es la siguiente:

## Arquitectura del webservice

La vista a nivel de componentes del webservice desarrollado es la siguiente:

![Webservice Components](/assets/main_components_backend.PNG)

De este diagrama podemos enumerar los siguientes componentes:


-  **International Business Men Backend**: Es el componente principal y a su vez la solución que contiene todos los proyectos creados para diseñar el webservice ofrecido para la aplicación, por lo cual podemos decir que es el contenedor de la solución del lado del Backend.

-  **International_Business_Men.API**: Es el componente que contiene la definición de los métodos expuestos por el webservice (endpoints) desarrollados con ASP.NET Core 3.1.

-  **International_Business_Men.DL**: Es el componente que contiene la definición de los repositorios y servicios desarrollados que abstraen la implementación trivial del acceso a la capa de datos al componente que expone los métodos (enpoints) a la aplicación web cliente.

-  **International_Business_Men.DAL**: Es el componente responsable de implementar la capa de acceso a datos en los niveles niveles definidos (Acceso a servicio externo y base de datos local SQL Server con Entity Framework Core), siendo este componente el único dentro de la solución que conoce el origen de los datos ofrecidos.

  

Para la realización de la solución se han utilizado diferentes librerías/frameworks/lenguajes tales como C#, ASP.NET Core 3.1, Entity Framework Core 3.1, SQL Server Express LocalDb, .NET Standard 2.1, NewtonSoft.Json, AutoMapper, Swagger, entre otros.

Adicionalmente, se desarrollaron pruebas unitarias utilizando XUnit como test runner y Moq para proveer data de prueba desde los servicios y repositorios.

Para realizar los logs de la aplicación se ha utilizado la librería Nlog en el server, la cual almacena todos los logs producidos dento del directorio `**c:\temp**`.

  

## Arquitectura de la aplicación web

  

La vista a nivel de componentes de la aplicación web es la siguiente:

  

![Web Apps Components](/assets/main_components_frontend.PNG)

  

De este diagrama podemos enumerar los siguientes componentes principal:

  

-  **International Business Men Web**: Es el componente principal y a su vez la solución que contiene el proyecto creado para diseñar la aplicación web, por lo cual podemos decir que es el contenedor de la solución del lado del Fronted.

-  **International_Business_Men.Web**: Es el componente que contiene la aplicación contenedora desarrollada en ASP.NET Core 3.1 Core MVC cuya única finalidad es renderizar la aplicación web SPA creada con REACT contenida dentro del directorio ClientApp.

Para la realización de la solución se han utilizado diferentes librerías/frameworks/lenguajes tales como Typescript 3.6, react, redux, redux-thunk, react-router, bootstrap, reactstrap, react-dom, redux-logger, entre otros. Además utiliza otras librerías/framework para el apoyo durante la fase de desarrollo y pruebas unitarias tales como create-react-app, jest, cross-env, redux-mock-store, react-test-renderer, entre otros.

Adicionalmente, se utilizan algunas librerías para mejorar la calidad del código producido como eslint y el propio hecho de utilizar typescript en lugar de javascript para el desarrollo principal de la solución web.

 
# Instrucciones de Instalación

## Prerrequisitos para el funcionamiento correcto de la solución
  

Para que el webservice y la aplicación web funcionen correctamente luego del clonado exitoso del repositorio se deben cumplir los siguientes requisitos críticos del sistema:

 
-  [Windows Client: 7, 8.1, 10 (1607+)](https://devblogs.microsoft.com/dotnet/announcing-net-core-3-1/)
- [NodeJs](https://nodejs.org/es/download/) version: ^8.12.0 || ^10.13.0 || >=11.10.1
- [Microsoft SQL Server Express LocalDb](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15) 13.0.4001.0 o superior
- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/visual-studio-sdks)
- [Visual studio 2019 16.4](https://visualstudio.microsoft.com/es/downloads/) ([*Recomendado*](https://devblogs.microsoft.com/dotnet/announcing-net-core-3-1/))

  

Adicionalmente, es recomendable tener configurado Github for Desktop o en su defecto tener el cliente de Git instalado localmente para el clonado del repositorio.

  

## Configuración del ambiente

 Primero debemos clonar el repositorio contenido en la URL [https://github.com/carlos0202/International-Business-Men.git](https://github.com/carlos0202/International-Business-Men.git).
 
 Luego, procedemos a restaurar las dependencias de ambas soluciones que se encuentran en el directorio [src](/src) del repositorio, el cual contiene carpetas individuales para cada parte:

 - Para el webservice, el archivo de solución se encuentra dentro del directorio [/src/International-Business-Men.Server](/src/International-Business-Men.Server). Se recomienda utilizar Visual Studio 2019 ya que facilita el proceso de restauración de dependencias y la corrida de las pruebas unitarias. Antes de la primera corrida exitosa del webservice se deberá crear en la instancia de SQL Server Express LocalDb la base de datos utilizada. Para ello, procedemos a utilizar el comando `Update-Database` desde el _Package Manager Console_ de Visual Studio y luego ya podemos correr el aplicativo utilizando Visual Studio. El resultado del comando deberá ser el siguiente:
	 
![EF Database Update](/assets/ef_database_restore.png)

- Para la aplicación cliente, el archivo de solución se encuentra dentro del directorio [/src/International-Business-Men.Client](/src/International-Business-Men.Client). Se recomienda también utilizar Visual Studio 2019 para una mejor experiencia al realizar el build de la solución y correrla en el navegador. 
Dentro del folder de la aplicación cliente se debe navegar al directorio [ClientApp](src/International-Business-Men.Client/International-Business-Men.WEB/ClientApp) y ejecutar el comando `npm install` para restaurar los paquetes de npm referenciados en el archivo **packages.json**. Luego para la ejecución del aplicativo web fuera del entorno de Visual Studio 2019 puede ejecutar el comando `npm run-script start` que inicia el servidor de desarrollo propio de la aplicación cliente y abre en una instancia del navegador por defecto la aplicación web en la URL raíz. Si se realiza la corrida del aplicativo desde Visual Studio no es necesario la corrida de los comandos anteriores, ya que Visual Studio se encarga de restaurar las dependencias y generar el build del proyecto cuando le damos a correr desde el IDE.

# Pruebas unitarias

Para la corrida de las pruebas unitarias del lado del webservice, se puede proceder a utilizar las opciones disponibles por el IDE Visual Studio en el panel _**Test > Test Explorer**_ y presionando la opcionón para correr todos los tests, o en su defecto presionando la opción en el panel _**Test > Run All Tests**_. El resultado de dichas corridas debe arrojar lo siguiente:

![Test Runner Server](/assets/service_unit_tests.png)

Se puede apreciar que para el webservice existen 2 proyectos separados que realizan las pruebas tanto al proyecto que expone los métodos a ser utilizados por el ciente (endpoints) como la capa intermedia entre el proyecto API y el proyecto que contiene la capa de acceso a datos.

Para la corrida de las pruebas del lado de la aplicación web, procedemos a abrir una consola de comandos en el directorio [ClientApp](src/International-Business-Men.Client/International-Business-Men.WEB/ClientApp) y ejecutamos el comando `npm run-script test` el cual inicia las pruebas unitarias utilizando el test-runner de Jest preconfigurado por el packete react-scrips. Al ejecutar el comando debe mostrarse un resultado similar al siguiente:

![Test Runner Client](/assets/client_unit_tests.png)

# Consideraciones finales 

Para la parte del servidor debemos tomar en cuenta que los métodos ofrecidos no realizan ningún tipo de cálculo u operación matemática, por lo cual su función principal es almacenar y obtener los datos de las 2 fuentes de datos configuradas. Una adición futura será que los datos sean persistidos en el webserver en archivos .json. 

Para la aplicación del cliente debemos tomar en cuenta que los navegadores soportados son los mostrados en el siguiente enlace: [Ver navegadores soportados](https://browserl.ist/?q=%3E0.2%25%2C+not+dead%2C+not+ie+%3C%3D+11%2C+not+op_mini+all). Esta lista fué realizada con [browserl.ist](https://github.com/browserslist/browserslist) que sirve como soporte al module bundler utilizado para generar el código Javascript a partir del código Typescript escrito en la aplicación cliente. La lista de navegadores soportados excluye a Internet Explorer ya que incluirlo en la lista de soporte suele provocar que no se utilicen las útimas versiones de frameworks y librerías para programar en el cliente. 

