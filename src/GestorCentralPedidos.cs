sealed class GestorCentralPedidos
{
    private static readonly GestorCentralPedidos _instancia = new GestorCentralPedidos();
    public static GestorCentralPedidos Instancia => _instancia;

    private readonly PedidoPool _pool;

    private GestorCentralPedidos()
    {
        _pool = new PedidoPool(tamanioInicial: 2, maxTamanio: 100);
    }

    public Pedido CrearYPrepararPedido(string cliente, string direccion, string producto)
    {
        Pedido pedido = _pool.Adquirir();
        pedido.Preparar(cliente, direccion, producto);
        return pedido;
    }

    public void Despachar(Pedido pedido) => pedido.Despachar();
    public void MarcarEntregado(Pedido pedido) => pedido.MarcarEntregado();

    public void CerrarYLiberar(Pedido pedido)
    {
        if (pedido.Estado != EstadoPedido.Entregado)
            throw new InvalidOperationException("No se puede liberar un pedido que no ha sido entregado.");
        _pool.Liberar(pedido);
    }
}