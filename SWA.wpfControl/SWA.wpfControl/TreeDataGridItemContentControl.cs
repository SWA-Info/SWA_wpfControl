using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SWA.wpfControl;

public class TreeDataGridItemContentControl : ContentControl
{
  static TreeDataGridItemContentControl()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeDataGridItemContentControl), new FrameworkPropertyMetadata(typeof(TreeDataGridItemContentControl)));
  }

  public TreeDataGridItemContentControl()
  {
    this.Loaded += TreeItemContentControl_Loaded;
  }

  private void TreeItemContentControl_Loaded(object sender, RoutedEventArgs e)
  {
    Loaded -= TreeItemContentControl_Loaded;
    var p = VisualTreeHelper.GetParent(this);
    if (p != null && p is FrameworkElement f)
    {
      f.Margin = new Thickness(0);

      if (p is ContentPresenter cp)
      {

      }
    }
  }

  #region TreeDataGridItemData DependencyProperty
  public TreeDataGridItemData TreeData
  {
    get { return (TreeDataGridItemData)GetValue(TreeDataProperty); }
    set { SetValue(TreeDataProperty, value); }
  }
  public static readonly DependencyProperty TreeDataProperty =
          DependencyProperty.Register("TreeData", typeof(TreeDataGridItemData), typeof(TreeDataGridItemContentControl),
          new PropertyMetadata(null, new PropertyChangedCallback(TreeDataGridItemContentControl.OnTreeDataPropertyChanged)));

  private static void OnTreeDataPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
  {
    if (obj is TreeDataGridItemContentControl)
    {
      (obj as TreeDataGridItemContentControl).OnTreeDataValueChanged();
    }
  }

  protected void OnTreeDataValueChanged()
  {

  }
  #endregion

  #region LevelIndentSize DependencyProperty
  public int LevelIndentSize
  {
    get { return (int)GetValue(LevelIndentSizeProperty); }
    set { SetValue(LevelIndentSizeProperty, value); }
  }
  public static readonly DependencyProperty LevelIndentSizeProperty =
          DependencyProperty.Register("LevelIndentSize", typeof(int), typeof(TreeDataGridItemContentControl),
          new PropertyMetadata(30, new PropertyChangedCallback(TreeDataGridItemContentControl.OnLevelIndentSizePropertyChanged)));

  private static void OnLevelIndentSizePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
  {
    if (obj is TreeDataGridItemContentControl)
    {
      (obj as TreeDataGridItemContentControl).OnLevelIndentSizeValueChanged();
    }
  }

  protected void OnLevelIndentSizeValueChanged()
  {

  }
  #endregion

  #region IconLineStroke DependencyProperty
  //public Brush IconLineStroke
  //{
  //  get { return (Brush)GetValue(IconLineStrokeProperty); }
  //  set { SetValue(IconLineStrokeProperty, value); }
  //}
  //public static readonly DependencyProperty IconLineStrokeProperty =
  //        DependencyProperty.Register("IconLineStroke", typeof(Brush), typeof(TreeItemContentControl),
  //        new PropertyMetadata(new SolidColorBrush(Colors.Blue), new PropertyChangedCallback(TreeItemContentControl.OnIconLineStrokePropertyChanged)));

  //private static void OnIconLineStrokePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
  //{
  //  if (obj is TreeItemContentControl)
  //  {
  //    (obj as TreeItemContentControl).OnIconLineStrokeValueChanged();
  //  }
  //}

  //protected void OnIconLineStrokeValueChanged()
  //{

  //}
  #endregion
}
