class Pedido
{
    public int Id { get; private set; }
    public string Cliente { get; private set; } = "";
    public string Direccion { get; private set; } = "";
    public string Producto { get; private set; } = "";
    public EstadoPedido Estado { get; private set; }
    public bool EnUso { get; set; }

    private static int _contador = 1;
    public Pedido()
    {
        Id = _contador++;
        Reset();
    }

    public void Preparar(string cliente, string direccion, string producto)
    {
        Cliente = cliente;
        Direccion = direccion;
        Producto = producto;
        Estado = EstadoPedido.Preparacion;
        Console.WriteLine($"Pedido en {this.Estado}: {this.Cliente}, {this.Direccion}, {this.Producto}");
    }

    public void Despachar()
    {
        if (Estado != EstadoPedido.Preparacion)
            throw new InvalidOperationException("Solo se puede despachar un pedido en preparación.");
        Estado = EstadoPedido.EnRuta;
        Console.WriteLine($"Pedido {this.Estado}: {this.Cliente}, {this.Direccion}, {this.Producto}");
    }

    public void MarcarEntregado()
    {
        if (Estado != EstadoPedido.EnRuta)
            throw new InvalidOperationException("Solo se puede marcar entregado un pedido en ruta.");
        Estado = EstadoPedido.Entregado;
        Console.WriteLine($"Pedido {this.Estado}: {this.Cliente}, {this.Direccion}, {this.Producto}");
    }

    public void Reset()
    {
        Cliente = "";
        Direccion = "";
        Producto = "";
        Estado = EstadoPedido.Disponible;
        EnUso = false;
    }
}
