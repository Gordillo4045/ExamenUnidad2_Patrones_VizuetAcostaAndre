using System;
using System.Collections.Generic;
class Program
{
    static void Main(string[] args)
    {
        GestorCentralPedidos gestor = GestorCentralPedidos.Instancia;

        Console.WriteLine("== Pedido 1 ==");
        Pedido pedido1 = gestor.CrearYPrepararPedido(
            cliente: "Edgar Manuel",
            direccion: "Sta. Ximena 49",
            producto: "Azzaro The Most Wanted EDP Intense 100ml"
        );
        gestor.Despachar(pedido1);
        gestor.MarcarEntregado(pedido1);
        gestor.CerrarYLiberar(pedido1);

        Console.WriteLine("\n== Pedido 2 ==");
        Pedido pedido2 = gestor.CrearYPrepararPedido(
            cliente: "Andre Vizuet",
            direccion: "Villas del Real",
            producto: "Jean Paul Gaultier Le Male Elixir 125ml"
        );
        gestor.Despachar(pedido2);
        gestor.MarcarEntregado(pedido2);
        gestor.CerrarYLiberar(pedido2);

        Console.WriteLine($"\nComprobación de reutilización: \n Pedido 1: {pedido1.Id}\n Pedido 2: {pedido2.Id}");
    }
}