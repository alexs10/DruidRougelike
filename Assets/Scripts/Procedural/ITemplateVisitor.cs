﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ITemplateVisitor {
    void Visit(TemplateHallway hallway);
    void Visit(TemplateRoom room);
    void Visit(PlayerSpawn playerSpawn);
    void Visit(TemplateWall wall);
}
