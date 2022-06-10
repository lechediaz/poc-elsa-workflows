## Tabla de contenido

- [Descripción](#descripción)
  - [Prueba de concepto](#prueba-de-concepto)
  - [Flujo de aprobación modificado](#flujo-de-aprobación-modificado)
  - [Estados de una solicitud](#estados-de-una-solicitud)
  - [Capturas de pantalla](#capturas-de-pantalla)
- [Contenido](#contenido)
  - [postman](#postman)
  - [scripts](#scripts)
  - [showing](#showing)
  - [src](#src)
  - [workflows](#workflows)
- [Ejecutar](#ejecutar)
  - [Primera ejecución](#primera-ejecución)
    - [Datos iniciales](#datos-iniciales)
    - [Importar flujos de trabajo](#importar-flujos-de-trabajo)
- [Links de interés](#links-de-interés)

# Descripción

En este repositorio se encuentra versionado una prueba de concepto que busca integrar flujos de trabajo construidos en **ELSA Workflow** a una aplicación Web, con el fin de experimentar la flexibilidad que ofrece este framework a la hora de definir procesos o flujos de trabajo para nuestras aplicaciones de nuestra compañía o propias.

> Imagen de referencia

<img src="./showing/Learning%20Elsa.png" />

## Prueba de concepto

Esta prueba de concepto consiste en una Aplicación Web perteneciente a una fábrica que sirve para controlar el stock de materias primas. Esta se integra con algunos flujos de trabajo definidos en ELSA Workflow para poder hacer seguimiento a las solicitudes de materias primas así:

1. Una vez que el **personal de producción** detecta la necesidad de materias primas, crean una **solicitud** para que un **supervisor** la apruebe.
2. En caso de ser aprobada la solicitud, se pasa al área de **logística** que son los encargados de realizar las negociaciones con los proveedores.
3. Finalmente cuando el proveedor despacha la solicitud, desde el área de producción notifican el recibido de las materias primas.

## Flujo de aprobación modificado

Por defecto el flujo de aprobación es el mismo tanto para personal de producción como para los supervisores, pero para demostrar la flexibilidad de ELSA, cambiamos este flujo para hacer que los supervisores no necesiten aprobarse ellos mismos sus solicitudes, sino que pasan directamente al área de logística.

Esto lo puedes hacer importando el flujo de trabajo [publicar-solicitud-modif.json)](workflows/publicar-solicitud-modif.json) sobre el previamete importado con nombre *Publicación y Aprobación de Solicitudes*, ver la sección [Importar flujos de trabajo](#importar-flujos-de-trabajo).

## Estados de una solicitud

Se contemplan los siguientes estados para una solicitud:

- **En borrador:** Indica que una persona del área de producción está construyendo la solicitud.
- **Publicada:** Indica que la persona del área de producción ha enviado la solicitud para que sea aprobada por un supervisor.
- **Aprobada:** Indica que el supervisor ha aprobado la solicitud.
- **Rechazada:** Indica que el supervisor ha rechazado la solicitud.
- **En negociación:** Indica que alguien del área de logística ha empezado negociaciones con los proveedores.
- **Despachada:** Indica que se completaron las negociaciones con un proveedor y se han despachado las materias primas de la solicitud.
- **Completada:** Indica que la persona que solicitó las materias primas las recibió.

## Capturas de pantalla

Si bien la aplicación no es dificil de usar, a continuación se presentan algunas capturas de su funcionamiento:

> Simulación de sesión activa

<img src="./showing/Captura-9.PNG" />

> Creando una solicitud

<img src="./showing/Captura-1.PNG" />

> Revisando una solicitud

<img src="./showing/Captura-2.PNG" />

> Revisando instancias de workflows

<img src="./showing/Captura-3.PNG" />

<img src="./showing/Captura-4.PNG" />

<img src="./showing/Captura-7.PNG" />

> Revisando correos

<img src="./showing/Captura-5.PNG" />

<img src="./showing/Captura-6.PNG" />

> Revisando materias primas

<img src="./showing/Captura-8.PNG" />

# Contenido

El repositorio contiene las siguientes carpetas:

## postman

En esta [carpeta](postman) se encuentran colecciones de Postman que sirven de ayuda para realizar el llamado de algunos Enpoint tanto de la API de la aplicación de la fábrica como de ELSA Workflow.

## scripts

En esta [carpeta](scripts) se encuentran scripts de SQL que serán necesarios ejecutar la primera vez que se levante el contenedor de Docker para SQL Server.

## showing

En esta [carpeta](showing) se encuentran una imagen de referencia que he utilizado para la presentación de esta prueba de concepto. Además otras imágenes que usan en este README.

## src

En esta [carpeta](src) se encuentra el código fuente para la realización de esta prueba de concepto, a continuación una explicación de cada uno de los archivos y carpetas que la componen:

- [ElsaDashboardAndServer](src/ElsaDashboardAndServer):
Proyecto de ASP.NET Core en su versión 5.0, usa los paquetes necesarios para integrar el framework [ELSA Workflow](https://elsa-workflows.github.io/elsa-core/) junto con su propio Dashboard para poder gestionar cómodamente los flujos de trabajo.

- [FactoryApp](src/FactoryApp):
Proyecto de ASP.NET Core en su versión 5.0, contiene una aplicación hecha en ReactJS en su versión 16 ubicada en [ClientApp](src/FactoryApp/ClientApp), juntos conforman la aplicación Web que, para este ejemplo, una fábrica usa para gestionar las materias primas.

- [docker-compose.yml](src/docker-compose.yml):
Archivo que permite orquestar el despliegue de cada uno de los contenedore de Docker que conforman la prueba de concepto.

- [LearningElsa.sln](src/LearningElsa.sln):
Solución de Visual Studio que permite trabajar cómodamente desde el IDE.

## workflows

En esta [carpeta](workflows) se encuentran los flujos de trabajo que se deben importar por primera vez en ELSA Worflow una vez esté levantado el contenedor de Docker. Los flujos a importar son:

- [publicar-solicitud.json](workflows\publicar-solicitud.json) Flujo de trabajo para llevar a cabo la publicación y aprobación de una solicitud de materias primas.
- [negociar-solicitud.json](workflows/negociar-solicitud.json) Flujo de trabajo para llevar a cabo la negociación, despacho y completado de una solicitud de materias primas.

> Los demás archivos en esta carpeta son ocasionales para probar comportamientos en los flujos.

# Ejecutar

> La información acontinuación asume que se esta usando un host Windows con docker-compose.

Para poder ejecutar la prueba de concepto primero se debe crear una red de Docker llamada *elsa-net*, esto lo podemos hacer ejecutando el siguiente comando:

```
docker network create elsa-net
```

Luego debemos levantar las instancias de Docker haciendo uso del archivo de docker-compose para que sea mucho más sencillo, para hacer esto ejecutamos el siguiente comando:

```
docker-compose -f ./src/docker-compose.yml up -d --build
```

Al finalizar la ejecución podrás visualizar los diferentes contenedores en Docker Desktop:

<img src="showing/docker-compose.PNG" />

O ejecutando el siguiente comando:

```
docker ps
```

<img src="showing/docker-ps.PNG" />

Como te puedes dar cuenta los contenedores estarían disponbiles así:

- Aplicación Web de la fábrica: Navegando a http://localhost:8010
- Servidor y Dashboard de ELSA Workflow: Navegando a http://localhost:8011
- Servidor de SQL Server: Disponible en *localhost* usando el puerto **1414**
- Servidor SMTP: Captura correos electrónicos enviados desde ELSA, puedes revisar los correos navegando a http://localhost:1080

## Primera ejecución

Los siguientes pasos los debes realizar sólo la primera vez que ejecutes la prueba de concepto, es decir, una vez hayas terminado de levantar los contenedores de Docker con el docker-compose.

### Datos iniciales

La base de datos de la aplicación de la fábrica en este momento se encuentra sin datos, para llenarla con datos iniciales puedes usar el script ubicado en el archivo [create-initial-data.sql](scripts/create-initial-data.sql).

### Importar flujos de trabajo

Actualmente ELSA se encuentra sin flujos de trabajo, se deben importar los flujos [publicar-solicitud.json](workflows\publicar-solicitud.json) y [negociar-solicitud.json](workflows/negociar-solicitud.json) para llevar a cabo la prueba de concepto, para lograrlo haz lo siguiente:

1. Navega a http://localhost:8011 y :
2. Presiona el menú *Workflow Definitions*.

<img src="showing/import-elsa-1.PNG" />

3. Presiona el botón *Create Worflow*.

<img src="showing/import-elsa-2.PNG" />

4. Presiona el botón *Import*.

<img src="showing/import-elsa-3.PNG" />

5. Escoge el archivo [publicar-solicitud.json](workflows\publicar-solicitud.json) y haz click en *Publish*.

<img src="showing/import-elsa-4.PNG" />

6. Repide los pasos 1-5, pero importando el archivo [negociar-solicitud.json](workflows/negociar-solicitud.json).

Finalmente debes ver lo siguiente desde el menú *Workflow Definitions*.

<img src="showing/import-elsa-5.PNG" />

# Links de interés

- [ELSA Workflow](https://elsa-workflows.github.io/elsa-core/) Página oficial de ELSA Workflow.
- [docker network create](https://docs.docker.com/engine/reference/commandline/network_create/) Documentación acerca de cómo crear una red de docker.
- [Dockerizing React App With .NET Core Backend](https://medium.com/bb-tutorials-and-thoughts/dockerizing-react-app-with-net-core-backend-f02767dd9415) Información acerca de cómo Dockerizar una aplicación de React junto con ASP.NET Core.