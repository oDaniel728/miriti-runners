using Godot;

/// <summary>Controls navigation between menu pages.</summary>
public partial class MenuPageController : Node
{
	private Control _currentPage;

	public void ShowPage(Control page)
	{
		if (page == null) return;
		_currentPage?.Hide();
		_currentPage = page;
		_currentPage.Show();
	}
}
