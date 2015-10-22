Dat.V1.Controls.FormViewItem = function () {
    var me = this;
    this.Container = null;
    this.ID = null;
    this.Appended = false;
    this.FindElement = function (selector) {
        return jQuery(jQuery(me.Element).find(selector));
    };
    this.AppendTo = function (container, callback) {
        me.Appended = true;
        container.append(me.Element);
        me.Element.animate(me.ItemTemplate.Animation, callback);
    }
    this.Element = null;
    this.ItemTemplate = null;
    this.DataItem = null;
    this.ItemType = null;
    this.Index = null;
    this.Fields = new Array();
};