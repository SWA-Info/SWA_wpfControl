using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SWA.wpfControl;

public class TreeDataGridItemData : ViewModelBase
{
  #region Properties
  private bool _IsExpanded = true;
  public bool IsExpanded
  {
    get
    {
      return _IsExpanded;
    }
    set
    {
      if (_IsExpanded == value)
      {
        return;
      }
      SetProperty(ref _IsExpanded, value);
      SetItemsVisible(value);
    }
  }

  private int _Level = 0;
  public int Level
  {
    get
    {
      return _Level;
    }
    set
    {
      SetProperty(ref _Level, value);
    }
  }

  private object _Data = null;
  public object Data
  {
    get
    {
      return _Data;
    }
    set
    {
      SetProperty(ref _Data, value);
    }
  }

  private TreeDataGridItemData _ParentItem = null;
  public TreeDataGridItemData ParentItem
  {
    get
    {
      return _ParentItem;
    }
    set
    {
      SetProperty(ref _ParentItem, value);
    }
  }

  private List<TreeDataGridItemData> _ParentItems = null;
  public List<TreeDataGridItemData> ParentItems
  {
    get
    {
      if (_ParentItems == null)
      {
        _ParentItems = new List<TreeDataGridItemData>();
      }
      return _ParentItems;
    }
    set
    {
      SetProperty(ref _ParentItems, value);
    }
  }

  private List<TreeDataGridItemData> _Items = null;
  public List<TreeDataGridItemData> Items
  {
    get
    {
      if (_Items == null)
      {
        _Items = new List<TreeDataGridItemData>();
      }
      return _Items;
    }
    set
    {
      SetProperty(ref _Items, value);
    }
  }

  public Visibility ToggleVisibility
  {
    get
    {
      return Items.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
    }
  }

  private bool _IsVisible = true;
  public bool IsVisible
  {
    get
    {
      return _IsVisible;
    }
    private set
    {
      if (_IsVisible == value)
      {
        return;
      }

      SetProperty(ref _IsVisible, value);
      SetItemsVisible(value);
    }
  }
  #endregion

  private void SetItemsVisible(bool visible)
  {
    if (IsExpanded == false || visible == false)
    {
      Items.ForEach(p => p.IsVisible = false);
    }
    else
    {
      Items.ForEach(p => p.IsVisible = true);
    }
  }
}

public class TreeItemDataCollection : ObservableCollection<TreeDataGridItemData>
{
  IEnumerable _SourceItems = null;

  string _ItemsPath = "";

  List<TreeDataGridItemData> _RootItems = new List<TreeDataGridItemData>();

  #region UseTree
  private bool _UseTree = false;
  public bool UseTree
  {
    get
    {
      return _UseTree;
    }
    set
    {
      _UseTree = value;
    }
  }
  #endregion

  public TreeItemDataCollection(IEnumerable sourceItems, string itemsPath)
  {
    _SourceItems = sourceItems;
    _ItemsPath = itemsPath;
    if (_SourceItems is INotifyCollectionChanged list)
    {
      list.CollectionChanged += List_CollectionChanged;
    }
    ResetItems();
  }

  private void List_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    if (sender == null) return;
    ResetItems();
  }

  private void ResetItems()
  {
    if (_SourceItems is null)
    {
      ClearAllItems();
      return;
    }

    var items = new List<TreeDataGridItemData>();
    _RootItems.Clear();

    var list = this.Items.ToDictionary(l => l.Data);
    foreach (var item in _SourceItems)
    {
      if (list.TryGetValue(item, out var treeItem))
      {
        list.Remove(item);
      }
      else
      {
        treeItem = new TreeDataGridItemData { Data = item };
      }

      treeItem.ParentItems.Clear();
      treeItem.Level = 0;
      items.Add(treeItem);
      _RootItems.Add(treeItem);

      foreach (var sitem in GetSubItems(treeItem, _ItemsPath, list).ToList())
      {
        items.Add(sitem);
      }
    }

    ResetSubItems(_RootItems);

    this.Items.Clear();
    items.ForEach(t => this.Items.Add(t));
  }

  private void ResetSubItems(List<TreeDataGridItemData> items)
  {
    for (int i = 0; i < items.Count; i++)
    {
      var item = items[i];
      item.IsExpanded = false;
    }
  }

  private IEnumerable<TreeDataGridItemData> GetSubItems(TreeDataGridItemData treeItem, string itemsPath, Dictionary<object, TreeDataGridItemData> list)
  {
    var data = treeItem.Data;
    var p = data.GetType().GetProperty(itemsPath);
    var pvalue = p.GetValue(data, null);
    treeItem.Items.Clear();
    if (pvalue is IEnumerable subItems)
    {
      foreach (var item in subItems)
      {
        if (list.TryGetValue(item, out var subitem))
        {
          list.Remove(item);
        }
        else
        {
          subitem = new TreeDataGridItemData { Data = item };
        }

        subitem.ParentItem = treeItem;
        subitem.Level = treeItem.Level + 1;

        subitem.ParentItems.Clear();
        foreach (var pitem in treeItem.ParentItems)
        {
          subitem.ParentItems.Add(pitem);
        }
        subitem.ParentItems.Add(treeItem);

        treeItem.Items.Add(subitem);
        yield return subitem;

        foreach (var sitem in GetSubItems(subitem, itemsPath, list))
        {
          yield return sitem;
        }
      }
      if (treeItem.Items.Count > 0)
      {
        ResetSubItems(treeItem.Items);
      }
    }
  }

  private void ClearAllItems()
  {
    this.Items.Clear();
  }
}

public class TreeItemDataConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value == null || parameter == null)
    {
      return value;
    }
    if (value is IEnumerable items)
    {
      return new TreeItemDataCollection(items, parameter.ToString());
    }
    return value;
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value;
  }
}
