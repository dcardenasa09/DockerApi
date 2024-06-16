# Aplicación Web (.Net 8 / React JS)
Esta Aplicación Web consta de dos peoryectos, uno en .Net 8 para el backend y el otro desarrollado en React JS para la parte del frontend

> [!IMPORTANT]
> Puedes acceder a la aplicacion con el siguiente usuario y contraseña:
> - admin@test.com
> - Test2024.

## Ejecutar Aplicación con Docker Compose
Este repositorio contiene un archivo .yml que puede ser ejecutado utilizando Docker Compose. 
Sigue las siguientes instrucciones para configurar y ejecutar la aplicación en tu entorno local.

> [!NOTE]
> Antes de empezar, asegúrate de tener instalados los siguientes requisitos:
> - Docker: [Instalación de Docker](https://docs.docker.com/get-docker/)
> - Docker Compose: [Instalación de Docker Compose](https://docs.docker.com/compose/install/)

### Ejecución

Sigue estos pasos para ejecutar la aplicación utilizando Docker Compose:

1. **Clona el Repositorio:**
   git clone https://github.com/dcardenasa09/nombre-repositorio.git

2. **Accede a la carpeta raiz del proyecto:**
   cd nombre-repositorio

3. **Ejecuta Docker Compose:**
   docker-compose up -d

4. **Accede a la Aplicación:**
   [Ingresa la dirección en tu navegador](http://localhost:3000)

## Notas, Tips y Alertas

>[!NOTE]
>
> - Persistencia de Datos:
>   Los datos almacenados en el contenedor de SQL Server (sqlserver) pueden ser persistentes si configuras un volumen Docker para el directorio de datos del contenedor.
>
> - Seguridad de Contraseñas:
>   Las contraseñas y credenciales en este ejemplo son solo para propósitos de desarrollo.
>
> - Variables de Entorno:
>   Revisa y ajusta las variables de entorno en el archivo docker-compose.yml según sea necesario para tu entorno específico.
>
> - Contribuciones y Problemas:
>  Si deseas contribuir con mejoras o encuentras algún problema, por favor abre un issue o envía un pull request a través de GitHub.


