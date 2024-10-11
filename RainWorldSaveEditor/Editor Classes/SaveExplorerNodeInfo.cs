using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveEditor;

public class SaveExplorerNodeTag(TreeNode node, object target, PropertyInfo propertyInfo, bool canBeDeleted)
{
    public TreeNode TreeNode { get; private set; } = node;

    public object Target { get; private set; } = target;
    public PropertyInfo PropertyInfo { get; private set; } = propertyInfo;

    public bool CanBeDeleted { get; set; } = canBeDeleted;
}
