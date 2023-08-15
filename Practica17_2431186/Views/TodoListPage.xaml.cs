using System.Collections.ObjectModel;
using Practica17_2431186.Models;
using Practica17_2431186.Data;


namespace Practica17_2431186.Views;

public partial class TodoListPage : ContentPage
{//declaramos la variable database
    TodoItemDatabase database;
    //con esto podremos alamacenar y controlar elemtos del tipo TodoItem
    public ObservableCollection<TodoItem> Items { get; set; } = new();
    //el constructor TodoListPage recibe un parametro TodoItemDatabaseque se utilizara para inicializar la pagina 
    public TodoListPage(TodoItemDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
        BindingContext = this;
    }

    //OnNavigatedTo se ejecuta cuando se muestra.
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {//llama al metodo de navegación para realizar tareas necesarias
        base.OnNavigatedTo(args);
        //obtiene de manera asincrona los elementos de la base usando el metodo GetItemsAsync de la instancia de database
        var items = await database.GetItemsAsync();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();//limpia la colección 
            foreach (var item in items)
                Items.Add(item); //agrega los elementos a la colección
        });
    }
    //con esto codigo se añaden nuevas notas
    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = new TodoItem()
        });
    }
    //Este codigo lo que hace es mostrarnos más detalles de las notas
 
    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not TodoItem item)
            return;

        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = item
        });
    }
}