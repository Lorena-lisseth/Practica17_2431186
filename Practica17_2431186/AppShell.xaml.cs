using Practica17_2431186.Views;
namespace Practica17_2431186;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        //este codigo RegisterRoute toma 2 parametros, el nombre de la ruta (nameof(TodoItemPage) que navegara a una pagina de TodoItemPage, esto nos ayuda a no tener que especificar 
        //el tipo de la pagina directamente.
        Routing.RegisterRoute(nameof(TodoItemPage), typeof(TodoItemPage));

    }
}
