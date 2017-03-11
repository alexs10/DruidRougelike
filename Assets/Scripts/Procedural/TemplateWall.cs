
public class TemplateWall : ITemplateElement {
    int x, y;
    
    public TemplateWall(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public void Accept(ITemplateVisitor visitor) {
        visitor.Visit(this);
    }

    public int GetX() {
        return x;
    }

    public int GetY() {
        return y;
    }
}

