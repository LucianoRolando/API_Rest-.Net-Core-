# API_Rest-.Net-Core-
Es una api rest creada desde visual studio, con .net core(C#),
consulta a una base de datos a traves de Entity Framework en Sql Server.
Todas las consultas son por medio de procedimientos almacenados
El script esta en el repositorio.
usa la misma base de datos que el repositorio "SistemaMensajeria".


Para hacer uso de las funciones hay que ingresar con nick y contraseña-> "/Login"[Post],
y ahi se crea un token(guarda en claims id y nick de usuario) que vence a los 120 minutos.

De no estar registrado se puede registrar desde el controller-> "/Usuarios"[Post],

Para ver detalles de la cuenta->"/Usuarios/Editar"[Get]

Se pueden editar los datos de la cuenta->"/Usuarios/Editar"[Put]

Si se quiere borrar la cuenta->"/Usuarios"[Delete]

El controller Mensajes devuelte los mensajes por orden de fecha enviados entre
el usuario que inicio secion (id token) y el usuario que corresponde al id enviado en la url->"/Mensajes/{id}"[Get]

Tambien envia mensajes con->"/Mensajes/{id}"[Post]
