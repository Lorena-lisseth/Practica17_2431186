using Practica17_2431186.Data;
using Practica17_2431186.Models;

namespace Practica17_2431186.Views;

[QueryProperty("Item", "Item")]
public partial class TodoItemPage : ContentPage
{
    TodoItem item;
    public TodoItem Item
    {
        get => BindingContext as TodoItem;
        set => BindingContext = value;
    }
    TodoItemDatabase database;
    //este codigo se asegura que todos los componentes esten listos y se establece la instancia de la base de datos todoItemDatabase que se usara para interactuar
    public TodoItemPage(TodoItemDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
    }
//este codigo revisara que todos los espacios esten llenos y si no lo estan se enviara un mensaje de error
//si lo estan la nota se agregara y nos llevara al inicio.
    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Item.Name))
        {
            await DisplayAlert("Name Required", "Please enter a name for the todo item.", "OK");
            return;
        }

        await database.SaveItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }
    //con este se revisara si el item tiene un ID y lo eliminara, regresando al inicio, pero si no tiene un ID se quedara donde esta.
    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Item.ID == 0)
            return;
        await database.DeleteItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }
    //con este boton cancelamos y nos devuelve a la pagina de inicio
    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}