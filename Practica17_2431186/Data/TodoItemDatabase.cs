using SQLite;
using Practica17_2431186.Models;

namespace Practica17_2431186.Data
{

    // Agregamos el atributo de QueryProperty para pasar los paremetros de la consulta a la pagina

    [QueryProperty("Item", "Item")]
    public class TodoItemDatabase
    {
        //usamos SQLiteAsyncConnection para proporcionar acceso asincrono una base de datos de SQLite
        SQLiteAsyncConnection Database;
        public TodoItemDatabase()
        {

        }
        //Init verificara si la variable database es nula, si lo es se hara una conexion a una base de SQLite,
        //ademas de crear una tabla para almacenar los elementos de TodoItem, esto devolvera un objeto task que representa una operacion asíncrona.
        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<TodoItem>();
        }

        //se hace una lista de objetos TodoItem que primero realizara el metodo init(para inicializar),
        //luego de esto usara el metodo de Table<TodoItem> para acceder a la tabla y luego se imprimira la lista.

        public async Task<List<TodoItem>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<TodoItem>().ToListAsync();
        }

       
        public async Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<TodoItem>().Where(t => t.Done).ToListAsync();

        }

        //GetItemAsync es una tarea, esta tomara un ID y con ello devolvera el elemento TodoItem con ese id de la base, perimero se revisara que la base este 
        //inicializada, para despues buscar el elemto deseado en la tabla.
        public async Task<TodoItem> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        //este codigo guarda o actualiza un objeto de todoitem en la base de datos, para devolver el resultado de la operación.
        public async Task<int> SaveItemAsync(TodoItem item)
        {
            await Init();
            if (item.ID != 0)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }
        //este metodo eliminara un objeto de la base de datos, para devolver el resultado de la operación
        public async Task<int> DeleteItemAsync(TodoItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
