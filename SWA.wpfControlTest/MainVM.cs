using SWA.wpfControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWA.wpfControlTest
{
  public class TestTreeData : ViewModelBase
  {
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }

    public ObservableCollection<TestTreeData> TreeDatas { get; set; } = new();
  } 
  internal class MainVM : ViewModelBase
  {
    private TestTreeData _treeData = new();
    public MainVM() 
    {
      _treeData = new() { Name = "CollectionNode", Type = "Root", Description = ""};

      TestTreeData nodetmpLv1, nodetmpLv2;

      nodetmpLv1 = new TestTreeData { Name = "Node 100", Type = "Lv1", Description = "" };
      this.TreeData.TreeDatas.Add(nodetmpLv1);

      nodetmpLv2 = new TestTreeData { Name = "Node 110", Type = "Lv2", Description = "" };
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 111", Type = "Lv3", Description = "" });
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 112", Type = "Lv3", Description = "" });
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 113", Type = "Lv3", Description = "" });
      nodetmpLv1.TreeDatas.Add(nodetmpLv2);

      nodetmpLv2 = new TestTreeData { Name = "Node 120", Type = "Lv2", Description = "" };
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 121", Type = "Lv3", Description = "" });
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 122", Type = "Lv3", Description = "" });
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 123", Type = "Lv3", Description = "" });
      nodetmpLv1.TreeDatas.Add(nodetmpLv2);

      nodetmpLv2 = new TestTreeData { Name = "Node 130", Type = "Lv2", Description = "" };
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 131", Type = "Lv3", Description = "" });
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 132", Type = "Lv3", Description = "" });
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 133", Type = "Lv3", Description = "" });
      nodetmpLv1.TreeDatas.Add(nodetmpLv2);

      nodetmpLv1 = new TestTreeData { Name = "Node 200", Type = "Lv1", Description = "" };
      this.TreeData.TreeDatas.Add(nodetmpLv1);

      nodetmpLv2 = new TestTreeData { Name = "Node 210", Type = "Lv2", Description = "" };
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 211", Type = "Lv3", Description = "" });
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 212", Type = "Lv3", Description = "" });
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 213", Type = "Lv3", Description = "" });
      nodetmpLv1.TreeDatas.Add(nodetmpLv2);

      nodetmpLv2 = new TestTreeData { Name = "Node 220", Type = "Lv2", Description = "" };
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 221", Type = "Lv3", Description = "" });
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 222", Type = "Lv3", Description = "" });
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 223", Type = "Lv3", Description = "" });
      nodetmpLv1.TreeDatas.Add(nodetmpLv2);

      nodetmpLv2 = new TestTreeData { Name = "Node 230", Type = "Lv2", Description = "" };
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 231", Type = "Lv3", Description = "" });
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 232", Type = "Lv3", Description = "" });
      nodetmpLv2.TreeDatas.Add(new TestTreeData { Name = "Node 233", Type = "Lv3", Description = "" });
      nodetmpLv1.TreeDatas.Add(nodetmpLv2);

      OnPropertyChanged(nameof(TreeData) );
    }

    public TestTreeData TreeData => _treeData;
  }
}
