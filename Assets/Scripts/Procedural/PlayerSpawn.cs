

public class PlayerSpawn: ITemplateElement {

    public void Accept(ITemplateVisitor visitor) {
        visitor.Visit(this);
    }
}

