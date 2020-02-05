# International Business Men APP

Alicación creada para ayudar a los ejecutivos de una empresa X que vuelan por todo el mundo. Los ejecutivos  de dicha empresa necesitan un listado de cada producto con el cual comercian, y el total de la suma de las ventas de estos productos.

Para ofrecer la solución a dicho requerimiento se creó una página web SPA y webservice REST. Este webservice devuelve los resultados en formato JSON. 

## Especificaciones del webservice creado

El webservice desarrollado para cumplir con los requisitos previamente planteados tiene los siguientes métodos disponibles:

 - Un método que permite obtener un listado de todas las transacciones disponibles.
 - Un método que permite obtener las transacciones de un producto específico.
 - Un método que permite obtener una lista de todos los rates de conversión entre las diferentes monedas.

Además, se ha agregado soporte a swagger para facilitar las pruebas con el webservice creado. para acceder al recurso se debe utilizar el endpoint /swagger desde la ruta raíz del servicio publicado. Para el caso del ambiente de desarrollo por defecto la url será:

[https://localhost:44347/swagger](https://localhost:44347/swagger) 

Dicho recurso muestra la siguiente interfaz:
![Swagger definition file](/assets/service_swagger_definition.PNG)
