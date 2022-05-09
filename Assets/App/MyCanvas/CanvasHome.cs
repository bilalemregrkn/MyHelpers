namespace App.MyCanvas
{
	public class CanvasHome : CanvasController
	{
		public override CanvasType MyCanvasType() => CanvasType.Home;

		public void OnClick_Next()
		{
			CanvasManager.Instance.Open(CanvasType.GameOver);
		}
	}
}