using Newtonsoft.Json;

namespace ClientesApi
{
    public class Cliente
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Email { get; set; }

        public string? CPF { get; set; }

        public string? RG { get; set; }

        public string? Telefone { get; set; }

        public string? Endereco { get; set; }



        public List<Cliente> listarCliente()
        {
            var filePath = Path.GetFullPath(@".\Data\Database.json");

            var json = File.ReadAllText(filePath);   

            var clients = JsonConvert.DeserializeObject<List<Cliente>>(json);

            return clients;
        }

        public bool ReescreverArquivo(List<Cliente> clients)
        {
            var filePath = Path.GetFullPath(@".\Data\Database.json");

            var json = JsonConvert.SerializeObject(clients, Formatting.Indented);
            File.WriteAllText(filePath, json);

            return true;
        }

        public Cliente Inserir(Cliente Cliente)
        {
            var listaClientes = this.listarCliente();

            var maxId = listaClientes.Max(cliente => cliente.Id);
            Cliente.Id = maxId + 1;
            listaClientes.Add(Cliente);

            ReescreverArquivo(listaClientes);
            return Cliente;
        }

        public Cliente Atualizar(int id, Cliente Cliente)
        {
            var listaClientes = this.listarCliente();

            var itemIndex = listaClientes.FindIndex(cliente => cliente.Id == id);
            if (itemIndex >= 0)
            {
                Cliente.Id = id;
                listaClientes[itemIndex] = Cliente;
            }
            else
            {
                return null;
            }

            ReescreverArquivo(listaClientes);
            return Cliente;
        }

        public bool Deletar(int id)
        {
            var listaClientes = this.listarCliente();

            var itemIndex = listaClientes.FindIndex(cliente => cliente.Id == id);
            if (itemIndex >=0 )
            {
                listaClientes.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            ReescreverArquivo(listaClientes);
            return true;
        }
    }
}
