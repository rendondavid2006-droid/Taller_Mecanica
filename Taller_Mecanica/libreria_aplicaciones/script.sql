CREATE DATABASE Taller_Mecanica
GO

USE Taller_Mecanica
GO


CREATE TABLE [Clientes](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Documento] NVARCHAR(15) not null,
	[Nombre] NVARCHAR(20) not null,
	[Apellido] NVARCHAR(20) not null,
	[Telefono] NVARCHAR(12) not null,
	[Correo]  NVARCHAR(30) not null,
	[Direccion] NVARCHAR(30) not null
)

CREATE TABLE [Marcas](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre_Marca] NVARCHAR(15) not null,
	[Pais_Origen] NVARCHAR(20) not null,
	[Descripcion] NVARCHAR(20) not null,
	[Estado] BIT not null
)

CREATE TABLE [Roles](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre_Rol] NVARCHAR(50) not null,
	[Descripcion] NVARCHAR(100) not null,
	[Nivel_Acceso] INT not null,
	[Estado] BIT not null
)

CREATE TABLE [Proveedores](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nit] NVARCHAR(15) not null,
	[Nombre_Proveedor] NVARCHAR(20) not null,
	[Telefono] NVARCHAR(20) not null,
	[Correo]  NVARCHAR(30) not null,
	[Direccion] NVARCHAR(30) not null
)

CREATE TABLE [Servicios](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre_Servicio] NVARCHAR(50) not null,
	[Descripcion] NVARCHAR(100) not null,
	[Precio_Base] DECIMAL(10,2) not null,
	[Tiempo_Estimado] SMALLDATETIME not null,
	[Estado] BIT not null
)


CREATE TABLE [Estados_Ordenes](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre_Estado] NVARCHAR(15) not null,
	[Descripcion] NVARCHAR(20) not null,
	[Orden_Proceso] INT not null,
	[Estado] BIT not null
)

CREATE TABLE [Repuestos](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Nombre_Repuesto] NVARCHAR(50) not null,
	[Descripcion] NVARCHAR(100) not null,
	[Precio_Compra] DECIMAL(10,2) not null,
	[Precio_Venta] DECIMAL(10,2) not null,
	[Stock] INT not null,
	[Stock_Minimo] INT not null
)

CREATE TABLE [Modelos_Vehiculos](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Marca] INT REFERENCES [Marcas](Id),
	[Nombre_Modelo] NVARCHAR(20) not null,
	[Año_inicio] INT not null,
	[Tipo_Vehiculo] NVARCHAR(100) not null,
	[Cilindraje] NVARCHAR(5) not null
)

CREATE TABLE [Vehiculos](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Cliente] INT REFERENCES [Clientes](Id),
	[Modelo] INT REFERENCES [Modelos_Vehiculos](Id),
	[Placa] NVARCHAR(6) not null,
	[Año] INT not null,
	[Color] NVARCHAR(30) not null,
	[Kilometraje] NVARCHAR(30) not null
)

CREATE TABLE [Empleados](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Rol] INT REFERENCES [Roles](Id),
	[Documento] NVARCHAR(12) not null,
	[Nombre] NVARCHAR(20) not null,
	[Apellido] NVARCHAR(20) not null,
	[Telefono] NVARCHAR(12) not null,
	[Correo]  NVARCHAR(30) not null
)

CREATE TABLE [Usuarios](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Empleado] INT REFERENCES [Empleados](Id),
	[Nombre_Usuario] NVARCHAR(30) not null,
	[Contraseña] NVARCHAR(30) not null,
	[Estado] BIT not null,
	[Fecha_Creacion] SMALLDATETIME not null
)

CREATE TABLE [Citas](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Cliente] INT REFERENCES [Clientes](Id),
	[Vehiculo] INT REFERENCES [Vehiculos](Id),
	[Fecha_Cita] SMALLDATETIME not null,
	[Hora_Cita] SMALLDATETIME not null,
	[Motivo] NVARCHAR(500) not null,
	[Estado] NVARCHAR(50) not null
)

CREATE TABLE [Compras](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Proveedor] INT REFERENCES [Proveedores](Id),
	[Fecha_Compra] SMALLDATETIME not null,
	[Numero_Factura_Proveedor] NVARCHAR(30) not null,
	[Total_Compra] Decimal(10,2) not null,
	[Estado] NVARCHAR(50) not null
)

CREATE TABLE [Detalles_Compras](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Compra] INT REFERENCES [Compras](Id),
	[Repuesto] INT REFERENCES [Repuestos](Id),
	[Cantidad] INT not null,
	[Precio_Unitario] DECIMAL(10,2) not null,
	[Subtotal] DECIMAL(10,2) not null
)

CREATE TABLE [Ordenes_Servicios](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Cita] INT REFERENCES [Citas](Id),
	[Vehiculo] INT REFERENCES [Vehiculos](Id),
	[Empleado] INT REFERENCES [Empleados](Id),
	[Estado_Orden] INT REFERENCES [Estados_Ordenes](Id),
	[Fecha_Ingreso] SMALLDATETIME not null,
	[Kilometraje_Ingreso] NVARCHAR(30) not null,
	[Observaciones] NVARCHAR(200) not null
)

CREATE TABLE [Diagnosticos](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Orden] INT REFERENCES [Ordenes_Servicios](Id),
	[Empleado] INT REFERENCES [Empleados](Id),
	[Fecha_Diagnostico] SMALLDATETIME not null,
	[Descripcion_Problema] NVARCHAR(100) not null,
	[Observaciones] NVARCHAR(200) not null
)

CREATE TABLE [Facturas](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Orden] INT REFERENCES [Ordenes_Servicios](Id),
	[Cliente] INT REFERENCES [Clientes](Id),
	[Fecha_Factura] SMALLDATETIME not null,
	[Subtotal] Decimal(10,2) not null,
	[Impuesto] Decimal(10,2) not null,
	[Estado] NVARCHAR(100) not null,
	[Total] Decimal(10,2) not null
)

CREATE TABLE [Pagos](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Factura] INT REFERENCES [Facturas](Id),
	[Fecha_Pago] SMALLDATETIME not null,
	[Monto] Decimal(10,2) not null,
	[Metodo_Pago] NVARCHAR(100) not null,
	[Referencia_Pago] NVARCHAR(100) not null,
	[Estado] NVARCHAR(100) not null
)

CREATE TABLE [Detalles_Ordenes_Servicios](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Orden] INT REFERENCES [Ordenes_Servicios](Id),
	[Servicio] INT REFERENCES [Servicios](Id),
	[Cantidad] INT not null,
	[Precio_Unitario] Decimal(10,2) not null,
	[Subtotal] Decimal(10,2) not null
)

CREATE TABLE [Detalles_Repuestos_Ordenes](
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Orden] INT REFERENCES [Ordenes_Servicios](Id),
	[Repuesto] INT REFERENCES [Repuestos](Id),
	[Cantidad] INT not null,
	[Precio_Unitario] Decimal(10,2) not null,
	[Subtotal] Decimal(10,2) not null
)

USE Taller_Mecanica
GO

-- Clientes
INSERT INTO [Clientes] ([Documento],[Nombre],[Apellido],[Telefono],[Correo],[Direccion]) VALUES
('123456789', 'Carlos', 'Ramirez', '3001234567', 'carlos@gmail.com', 'Calle 10 #5-20'),
('987654321', 'Maria', 'Gomez', '3007654321', 'maria@gmail.com', 'Carrera 15 #8-33')
GO

-- Marcas
INSERT INTO [Marcas] ([Nombre_Marca],[Pais_Origen],[Descripcion],[Estado]) VALUES
('Toyota', 'Japon', 'Vehiculos livianos', 1),
('Chevrolet', 'Estados Unidos', 'Vehiculos livianos', 1)
GO

-- Roles
INSERT INTO [Roles] ([Nombre_Rol],[Descripcion],[Nivel_Acceso],[Estado]) VALUES
('Administrador', 'Acceso total', 1, 1),
('Mecanico', 'Acceso operativo', 2, 1)
GO

-- Proveedores
INSERT INTO [Proveedores] ([Nit],[Nombre_Proveedor],[Telefono],[Correo],[Direccion]) VALUES
('900123456-1', 'Autopartes SA', '3101234567', 'ventas@autopartes.com', 'Calle 50 #12-30'),
('900654321-2', 'Repuestos JR', '3117654321', 'contacto@repuestosjr.com', 'Avenida 30 #20-15')
GO

-- Servicios
-- Nota: Tiempo_Estimado esta definido como SMALLDATETIME en la tabla original,
-- por eso se inserta una fecha base (1900-01-01) con la hora que representa la duracion.
INSERT INTO [Servicios] ([Nombre_Servicio],[Descripcion],[Precio_Base],[Tiempo_Estimado],[Estado]) VALUES
('Cambio Aceite', 'Cambio de aceite', 50000.00, '19000101 00:30:00', 1),
('Alineacion', 'Alineacion llantas', 80000.00, '19000101 01:00:00', 1)
GO

-- Estados_Ordenes
INSERT INTO [Estados_Ordenes] ([Nombre_Estado],[Descripcion],[Orden_Proceso],[Estado]) VALUES
('Recibido', 'Vehiculo recibido', 1, 1),
('En Proceso', 'En reparacion', 2, 1)
GO

-- Repuestos
INSERT INTO [Repuestos] ([Nombre_Repuesto],[Descripcion],[Precio_Compra],[Precio_Venta],[Stock],[Stock_Minimo]) VALUES
('Filtro Aceite', 'Filtro de aceite', 8000.00, 15000.00, 50, 10),
('Pastillas', 'Pastillas de freno', 12000.00, 20000.00, 30, 8)
GO

-- Modelos_Vehiculos (depende de Marcas)
INSERT INTO [Modelos_Vehiculos] ([Marca],[Nombre_Modelo],[Año_inicio],[Tipo_Vehiculo],[Cilindraje]) VALUES
(1, 'Corolla', 2015, 'Sedan', '1800'),
(2, 'Spark', 2018, 'Hatchback', '1200')
GO

-- Vehiculos (depende de Clientes y Modelos_Vehiculos)
INSERT INTO [Vehiculos] ([Cliente],[Modelo],[Placa],[Año],[Color],[Kilometraje]) VALUES
(1, 1, 'ABC123', 2015, 'Rojo', '45000'),
(2, 2, 'XYZ789', 2018, 'Blanco', '30000')
GO

-- Empleados (depende de Roles)
INSERT INTO [Empleados] ([Rol],[Documento],[Nombre],[Apellido],[Telefono],[Correo]) VALUES
(1, '111222333', 'Juan', 'Perez', '3201234567', 'juan.perez@taller.com'),
(2, '444555666', 'Andres', 'Lopez', '3209876543', 'andres.lopez@taller.com')
GO

-- Usuarios (depende de Empleados)
INSERT INTO [Usuarios] ([Empleado],[Nombre_Usuario],[Contraseña],[Estado],[Fecha_Creacion]) VALUES
(1, 'jperez', 'Clave123*', 1, '20240110'),
(2, 'alopez', 'Clave456*', 1, '20240112')
GO

-- Citas (depende de Clientes y Vehiculos)
INSERT INTO [Citas] ([Cliente],[Vehiculo],[Fecha_Cita],[Hora_Cita],[Motivo],[Estado]) VALUES
(1, 1, '20240201', '20240201 09:00:00', 'Revision general del vehiculo', 'Confirmada'),
(2, 2, '20240203', '20240203 10:30:00', 'Cambio de aceite y filtros', 'Confirmada')
GO

-- Compras (depende de Proveedores)
INSERT INTO [Compras] ([Proveedor],[Fecha_Compra],[Numero_Factura_Proveedor],[Total_Compra],[Estado]) VALUES
(1, '20240115', 'FAC-001', 80000.00, 'Pagada'),
(2, '20240120', 'FAC-002', 180000.00, 'Pagada')
GO

-- Detalles_Compras (depende de Compras y Repuestos)
INSERT INTO [Detalles_Compras] ([Compra],[Repuesto],[Cantidad],[Precio_Unitario],[Subtotal]) VALUES
(1, 1, 10, 8000.00, 80000.00),
(2, 2, 15, 12000.00, 180000.00)
GO

-- Ordenes_Servicios (depende de Citas, Vehiculos, Empleados, Estados_Ordenes)
INSERT INTO [Ordenes_Servicios] ([Cita],[Vehiculo],[Empleado],[Estado_Orden],[Fecha_Ingreso],[Kilometraje_Ingreso],[Observaciones]) VALUES
(1, 1, 2, 1, '20240201 09:15:00', '45000', 'Vehiculo ingresa para revision general'),
(2, 2, 2, 2, '20240203 10:45:00', '30000', 'Cambio de aceite y filtros programado')
GO

-- Diagnosticos (depende de Ordenes_Servicios y Empleados)
INSERT INTO [Diagnosticos] ([Orden],[Empleado],[Fecha_Diagnostico],[Descripcion_Problema],[Observaciones]) VALUES
(1, 2, '20240201 10:00:00', 'Frenos desgastados', 'Se recomienda cambio de pastillas de freno'),
(2, 2, '20240203 11:00:00', 'Aceite en mal estado', 'Se procede a realizar cambio de aceite')
GO

-- Facturas (depende de Ordenes_Servicios y Clientes)
INSERT INTO [Facturas] ([Orden],[Cliente],[Fecha_Factura],[Subtotal],[Impuesto],[Estado],[Total]) VALUES
(1, 1, '20240201 12:00:00', 90000.00, 17100.00, 'Pagada', 107100.00),
(2, 2, '20240203 13:00:00', 65000.00, 12350.00, 'Pagada', 77350.00)
GO

-- Pagos (depende de Facturas)
INSERT INTO [Pagos] ([Factura],[Fecha_Pago],[Monto],[Metodo_Pago],[Referencia_Pago],[Estado]) VALUES
(1, '20240201 12:30:00', 107100.00, 'Tarjeta de credito', 'REF-0001', 'Completado'),
(2, '20240203 13:30:00', 77350.00, 'Efectivo', 'REF-0002', 'Completado')
GO

-- Detalles_Ordenes_Servicios (depende de Ordenes_Servicios y Servicios)
INSERT INTO [Detalles_Ordenes_Servicios] ([Orden],[Servicio],[Cantidad],[Precio_Unitario],[Subtotal]) VALUES
(1, 1, 1, 50000.00, 50000.00),
(2, 1, 1, 50000.00, 50000.00)
GO

-- Detalles_Repuestos_Ordenes (depende de Ordenes_Servicios y Repuestos)
INSERT INTO [Detalles_Repuestos_Ordenes] ([Orden],[Repuesto],[Cantidad],[Precio_Unitario],[Subtotal]) VALUES
(1, 2, 2, 20000.00, 40000.00),
(2, 1, 1, 15000.00, 15000.00)
GO