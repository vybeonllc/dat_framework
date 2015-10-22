Dat.V1.Controls.TextBox = function (options) {

    var me = this;
    this.TagName = "textbox";

    this.Element = jQuery(options.Element);
    var id_suggestion_box = Dat.V1.Utils.Element.GetUniqueID("suggestion_box");
    this.SuggestionBox = options.SuggestionBox ? options.SuggestionBox : jQuery("<div>").css({ "position": "absolute", height: "150px", "z-index": "999999999" }).attr("id", id_suggestion_box);
    this.SuggestionElement = options.SuggestionElement ? options.SuggestionElement : jQuery("<div>")
                        .css({ "border": "1px", "background-color": "orange", cursor: "pointer", display: "inline" });
    this.MaximumSuggestions = options.MaximumSuggestions ? options.MaximumSuggestions : 10;
    this.OnItemDataBinding = options.OnItemDataBinding;
    this.OnItemDataBound = options.OnItemDataBound;
    this.OnDataBinding = options.OnDataBinding;
    this.OnDataBound = options.OnDataBound;
    this.OnSelect = options.OnSelect;
    this.OnOpen = options.OnOpen;
    this.OnClose = options.OnClose;
    this.Close = function () {
        me.SuggestionBox.find("*").slideUp("fast", function () { me.SuggestionBox.empty(); });
    };
    this.OnCalling = options.OnCalling;
    this.OnIdeaRequested = options.OnIdeaRequested;
    this.SuggestionBoxStyles = options.SuggestionBoxStyles;
    this.SuggestionBoxElementStyles = options.SuggestionBoxElementStyles;
    this.SuggestionBoxElementClass = options.SuggestionBoxElementClass;
    this.OnReady = options.OnReady;
    this.SuggestionBoxClass = options.SuggestionBoxClass;
    this.Delay = options.Delay;
    this.MinimumCharacters = options.MinimumCharacters ? options.MinimumCharacters : 4;
    if (options.SuggestionBoxClass)
        me.SuggestionBox.addClass(me.SuggestionBoxClass);
    if (me.SuggestionBoxStyles)
        me.SuggestionBox.css(me.SuggestionBoxStyles);
    jQuery(document).click(function (e) {
        var target = e.target;
        if (!$(target).is('#' + id_suggestion_box) && !$(target).parents().is('#' + id_suggestion_box)) {
            me.Close();
        }
    });
    me.SuggestionBox
        .blur(function () {
            me.Close();
        })
        .css({ "overflow": "auto" });
    me.Element
        .keyup(function () {
            me.SuggestionBox.empty();
            if (me.OnClose)
                me.OnClose();
            var field = { Idea: null, Field: jQuery(this) };
            me.OnIdeaRequested(field);
            var idea = field.Idea;
            if (idea == "" || idea.length < me.MinimumCharacters)
                return;
            var event_handler = { Idea: idea };
            if (me.OnCalling)
                me.OnCalling(me, event_handler);
            options.DataBinder({
                OnSuccess: function (response) {
                    jQuery(response.result_set.results).each(function (i, v) {
                        if (i == 0)
                            if (me.OnOpen)
                                me.OnOpen(me);
                        if (me.OnDataBinding)
                            me.OnDataBinding(me, response);
                        var suggestion_element = me.SuggestionElement.clone(true);
                        if (me.SuggestionBoxElementStyles)
                            suggestion_element.css(me.SuggestionBoxElementStyles);
                        if (me.SuggestionBoxElementClass)
                            suggestion_element.addClass(me.SuggestionBoxElementClass);
                        if (me.OnItemDataBinding)
                            me.OnItemDataBound(me, suggestion_element, v);
                        me.SuggestionBox.append(suggestion_element);
                        suggestion_element
                            .data("DataItem", v)
                            .click(function () {
                                jQuery(me.SuggestionBox.find(me.SuggestionElement.prop("tagName"))).fadeOut("fast");
                                if (me.OnSelect)
                                    me.OnSelect(me, suggestion_element, v, field.Idea);
                                me.Close();
                            }).slideDown();
                        if (me.OnItemDataBound)
                            me.OnItemDataBound(me, suggestion_element, v);
                        if (me.Delay)
                            window.setTimeout(function () {
                                me.Close();
                            }, me.Delay);
                    });
                    if (me.OnDataBound)
                        me.OnDataBound(me, response);
                },
                OnError: function (state, error) {
                    if (options.OnError)
                        options.OnError(state, error);
                },
                Idea: event_handler.Idea,
                MaximumSuggestions: me.MaximumSuggestions
            });
        });
    me.OnReady(me);
};