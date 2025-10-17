class PedidoPool
{
    private readonly Stack<Pedido> _disponibles = new Stack<Pedido>();
    private readonly Stack<Pedido> _enUso = new Stack<Pedido>();
    private readonly object _lock = new object();
    private readonly int _maxTamanio;

    public int DisponiblesCount { get { lock (_lock) return _disponibles.Count; } }
    public int EnUsoCount { get { lock (_lock) return _enUso.Count; } }

    public PedidoPool(int tamanioInicial, int maxTamanio)
    {
        _maxTamanio = maxTamanio;
        for (int i = 0; i < tamanioInicial; i++)
            _disponibles.Push(new Pedido());
    }
    public Pedido Adquirir()
    {
        lock (_lock)
        {
            Pedido p;
            if (DisponiblesCount > 0)
                p = _disponibles.Pop();
            else
            {
                int totalActual = DisponiblesCount + EnUsoCount;
                if (totalActual >= _maxTamanio)
                    throw new InvalidOperationException("El pool alcanzó su tamaño máximo.");

                p = new Pedido();
            }
            p.EnUso = true;
            _enUso.Push(p);
            return p;
        }
    }

    public void Liberar(Pedido pedido)
    {
        if (pedido == null) return;

        lock (_lock)
        {
            bool EncontrarYEliminar()
            {
                if (EnUsoCount == 0) return false;
                Pedido top = _enUso.Pop();
                if (top == pedido)
                {
                    return true;
                }
                bool encontradoEnResto = EncontrarYEliminar();
                _enUso.Push(top);
                return encontradoEnResto;
            }
            pedido.Reset();
            _disponibles.Push(pedido);
        }
    }
}