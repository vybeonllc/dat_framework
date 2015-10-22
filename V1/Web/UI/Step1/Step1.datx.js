Dat.V1.UI.Prospectfuel = {
    GoToNextStep: function ($current) {
        if (!$current)
            $current = jQuery("form[id|=form_Step1_");
        var $next = $current.next();
        if (!$next.hasClass("step")) {
            alert("No more step.");
            return;
        }
        $next.css("opacity", 0);
        $current.animate({ "margin-left": "-100%", "opacity": 0 }, function () { $current.hide(); });
        $next.show();
        $next.css("margin-left", "100%");
        $next.animate({ "margin-left": "0px", "opacity": 1 });
        $next.find("form").data("control").DataBind();
    },
    GoToPreviousStep: function ($current) {
        var $prev = $current.prev();
        if (!$prev.hasClass("step")) {
            alert("No more step.");
            return;
        }
        $prev.css("opacity", 0);
        $current.animate({ "margin-left": "100%", "opacity": 0 }, function () { $current.hide(); });
        $prev.show();
        $prev.css("margin-left", "-100%");
        $prev.animate({ "margin-left": "0px", "opacity": 1 });
        $prev.find("form").data("control").DataBind();
    },
    NewLead: function () {
        jQuery("form").each(function (i, v) {
            var $form = jQuery(v);
            if (!$form.parent().hasClass("step"))
                return;
            $form.parent().fadeOut().css({ "margin-left": "0px", "opacity": 1 });
        });
        jQuery("#lead_phone").empty();
        jQuery("#search").fadeIn();
    },
    AppendYesNoDropDown: function ($field) {
        jQuery("<select class='correctdata'><option  value='false' selected=true>Is This Correct?</option><option value='true'>Yes</option><option value='false'>No</option></select>").insertAfter($field);
    },
    Step1: {
        FormView1: {
            EventHandlers: {
                OnInit: function (fv) {
                    fv.Container.append(fv.Element);
                },
                OnReady: function (fv) {
                },
                OnCreating: function (sender, event_arg) {
                    var cancel = false;
                    sender.Element.find(".correctdata").each(function (i, v) {
                        if (cancel)
                            return;
                        if (jQuery(v).val() == "false") {
                            event_arg.Cancel = true;
                            cancel = true;
                        }
                    });
                    if (cancel)
                        alert("Please make sure all of the fields have correct information");
                },
                OnUpdating: function (sender, event_arg) {
                    var cancel = false;
                    sender.Element.find(".correctdata").each(function (i, v) {
                        if (cancel)
                            return;
                        if (jQuery(v).val() == "false") {
                            event_arg.Cancel = true;
                            cancel = true;
                        }
                    });
                    if (cancel)
                        alert("Please make sure all of the fields have correct information");
                },
                OnCreated: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToNextStep(event_arg.Container);
                },
                OnUpdated: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToNextStep(event_arg.Container);
                },
                OnFieldBound: function (field, value, $field, formviewitem, formview) {
                    if (!value || value == "")
                        return;
                    Dat.V1.UI.Prospectfuel.AppendYesNoDropDown($field);
                },
                OnItemCommand: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToPreviousStep(event_arg.FormView.Container);
                    console.log(event_arg);
                }
            }
        },
        OnLoad: function () {
            var $steps = jQuery(".step"),
                $search = jQuery("#search"),
                $username = jQuery("#username"),
                $password = jQuery("#password"),
                $wrapper = jQuery("#wrapper"),
                $loginbackgroundbox1 = jQuery(".backgroundBox1"),
                $loginbackgroundbox2 = jQuery(".backgroundBox2"),
                $logintext = jQuery(".loginText"),
                $user_extension = jQuery("#user_extension"),
                $lead_phone = jQuery("#lead_phone"),
                $lead_phone_container = jQuery("#lead_phone_container"),
                $user_extension_container = jQuery("#user_extension_container"),
                $new_lead = jQuery("#new_lead");

            $new_lead.hide();
            $steps.hide();
            $search.hide();
            $user_extension_container.hide();
            $lead_phone_container.hide();
            function onlogin(e) {
                if (e.which != 13)
                    return;
                if ($username.val() == "" && $password.val() == "")
                    return;
                Dat.V1.AssetPool.Manager.Call("Dat", "Dat.V1.Services.Dat.Membership.Authenticate.Authenticate", {
                    Data: { authentication_info: { email_address: $username.val(), password: $password.val() } },
                    OnSuccess: function (auth_response) {
                        $loginbackgroundbox1.fadeOut();
                        $loginbackgroundbox2.fadeOut();
                        $logintext.fadeOut(function () {
                            Dat.V1.AssetPool.Manager.Call("Dat", "Dat.V1.Services.Dat.Membership.User.Select", {
                                EmailAddress: auth_response.result_set.results[0].email_address,
                                OnSuccess: function (dat_user_response) {
                                    Dat.V1.AssetPool.Manager.Call("Leads", "Dat.V1.Services.Leads.Membership.User.Select",
                                        {
                                            User_Guid: dat_user_response.result_set.results[0].user_guid,
                                            OnSuccess: function (leads_user_response) {
                                                $search.fadeIn();
                                                $user_extension_container.fadeIn();
                                                $new_lead.fadeIn().click(Dat.V1.UI.Prospectfuel.NewLead);
                                                $user_extension.text(leads_user_response.result_set.results[0].extension);
                                            },
                                            OnError: function (sta, er) {
                                                alert(error);
                                            }
                                        });
                                },
                                OnError: function (sta, er) {
                                    alert(error);
                                }
                            });
                        });


                    },
                    OnError: function (state, error) {
                        alert(error);
                    }
                });
            };
            $username.keypress(onlogin);
            $password.keypress(onlogin);
            jQuery("#search button").click(function () {
                Dat.V1.AssetPool.Manager.Call("Leads", "Dat.V1.Services.Leads.Leads.Organization.Select", {
                    Parameters: new Array(jQuery("#search input").val()),
                    OnSuccess: function (response) {
                        $search.fadeOut(function () {
                            jQuery("form").each(function (i, v) {
                                var $form = jQuery(v);
                                if (!$form.parent().hasClass("step"))
                                    return;
                                var organization_id = response.result_set.results.length == 0 ? 0 : response.result_set.results[0].organization_id;
                                $form.init();
                                $form.data("control").DataBinder.SelectParameters = new Array();
                                $form.data("control").DataBinder.SelectParameters.push(organization_id);
                                $lead_phone_container.fadeIn();
                                $lead_phone.text(jQuery("#search input").val());
                            });
                            jQuery(jQuery(".step")[0]).show();
                            jQuery(jQuery(".step")[0]).find("form").data("control").DataBind();
                        });
                    },
                    OnError: function (state, error) {
                        alert(error);
                    }
                });
            });
        }
    },
    Step2: {
        FormView1: {
            EventHandlers: {
                OnInit: function (fv) {
                    fv.Container.append(fv.Element);
                },
                OnReady: function (fv) {
                },
                OnCreating: function (sender, event_arg) {
                    var cancel = false;
                    sender.Element.find(".correctdata").each(function (i, v) {
                        if (cancel)
                            return;
                        if (jQuery(v).val() == "false") {
                            event_arg.Cancel = true;
                            cancel = true;
                        }
                    });
                    if (cancel)
                        alert("Please make sure all of the fields have correct information");
                },
                OnUpdating: function (sender, event_arg) {
                    var cancel = false;
                    sender.Element.find(".correctdata").each(function (i, v) {
                        if (cancel)
                            return;
                        if (jQuery(v).val() == "false") {
                            event_arg.Cancel = true;
                            cancel = true;
                        }
                    });
                    if (cancel)
                        alert("Please make sure all of the fields have correct information");
                },
                OnCreated: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToNextStep(event_arg.Container);
                },
                OnUpdated: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToNextStep(event_arg.Container);
                },
                OnFieldBound: function (field, value, $field, formviewitem, formview) {
                    if (!value || value == "")
                        return;
                    Dat.V1.UI.Prospectfuel.AppendYesNoDropDown($field);
                },
                OnItemCommand: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToPreviousStep(event_arg.FormView.Container);
                    console.log(event_arg);
                }
            }
        }
    },
    Step3: {
        FormView1: {
            EventHandlers: {
                OnInit: function (fv) {
                    fv.Container.append(fv.Element);
                },
                OnReady: function (fv) {
                },
                OnCreating: function (sender, event_arg) {
                    var cancel = false;
                    sender.Element.find(".correctdata").each(function (i, v) {
                        if (cancel)
                            return;
                        if (jQuery(v).val() == "false") {
                            event_arg.Cancel = true;
                            cancel = true;
                        }
                    });
                    if (cancel)
                        alert("Please make sure all of the fields have correct information");
                },
                OnUpdating: function (sender, event_arg) {
                    var cancel = false;
                    sender.Element.find(".correctdata").each(function (i, v) {
                        if (cancel)
                            return;
                        if (jQuery(v).val() == "false") {
                            event_arg.Cancel = true;
                            cancel = true;
                        }
                    });
                    if (cancel)
                        alert("Please make sure all of the fields have correct information");
                },
                OnCreated: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToNextStep(event_arg.Container);
                },
                OnUpdated: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToNextStep(event_arg.Container);
                },
                OnFieldBound: function (field, value, $field, formviewitem, formview) {
                    if (!value || value == "")
                        return;
                    Dat.V1.UI.Prospectfuel.AppendYesNoDropDown($field);
                },
                OnItemCommand: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToPreviousStep(event_arg.FormView.Container);
                    console.log(event_arg);
                }
            }
        }
    },
    Step4: {
        FormView1: {
            EventHandlers: {
                OnInit: function (fv) {
                    fv.Container.append(fv.Element);
                },
                OnReady: function (fv) {
                },
                OnCreating: function (sender, event_arg) {
                    var cancel = false;
                    sender.Element.find(".correctdata").each(function (i, v) {
                        if (cancel)
                            return;
                        if (jQuery(v).val() == "false") {
                            event_arg.Cancel = true;
                            cancel = true;
                        }
                    });
                    if (cancel)
                        alert("Please make sure all of the fields have correct information");
                },
                OnUpdating: function (sender, event_arg) {
                    var cancel = false;
                    sender.Element.find(".correctdata").each(function (i, v) {
                        if (cancel)
                            return;
                        if (jQuery(v).val() == "false") {
                            event_arg.Cancel = true;
                            cancel = true;
                        }
                    });
                    if (cancel)
                        alert("Please make sure all of the fields have correct information");
                },
                OnCreated: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToNextStep(event_arg.Container);
                },
                OnUpdated: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToNextStep(event_arg.Container);
                },
                OnFieldBound: function (field, value, $field, formviewitem, formview) {
                    if (!value || value == "")
                        return;
                    Dat.V1.UI.Prospectfuel.AppendYesNoDropDown($field);
                },
                OnItemCommand: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToPreviousStep(event_arg.FormView.Container);
                    console.log(event_arg);
                }
            }
        }
    },
    Step5: {
        FormView1: {
            EventHandlers: {
                OnInit: function (fv) {
                    fv.Container.append(fv.Element);
                },
                OnReady: function (fv) {
                },
                OnCreating: function (sender, event_arg) {
                    var cancel = false;
                    sender.Element.find(".correctdata").each(function (i, v) {
                        if (cancel)
                            return;
                        if (jQuery(v).val() == "false") {
                            event_arg.Cancel = true;
                            cancel = true;
                        }
                    });
                    if (cancel)
                        alert("Please make sure all of the fields have correct information");
                },
                OnUpdating: function (sender, event_arg) {
                    var cancel = false;
                    sender.Element.find(".correctdata").each(function (i, v) {
                        if (cancel)
                            return;
                        if (jQuery(v).val() == "false") {
                            event_arg.Cancel = true;
                            cancel = true;
                        }
                    });
                    if (cancel)
                        alert("Please make sure all of the fields have correct information");
                },
                OnCreated: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToNextStep(event_arg.Container);
                },
                OnUpdated: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToNextStep(event_arg.Container);
                },
                OnFieldBound: function (field, value, $field, formviewitem, formview) {
                    if (!value || value == "")
                        return;
                    Dat.V1.UI.Prospectfuel.AppendYesNoDropDown($field);
                },
                OnItemCommand: function (event_arg) {
                    Dat.V1.UI.Prospectfuel.GoToPreviousStep(event_arg.FormView.Container);
                    console.log(event_arg);
                }
            }
        }
    }
};