DAT.v1.Utils = {
    DateTime: {
        ToAMPM: function (date) {
            var hours = date.getHours(),
            minutes = date.getMinutes(),
            seconds = date.getSeconds(),
            ampm = hours >= 12 ? 'PM' : 'AM';
            hours = hours % 12;
            hours = hours ? hours : 12;
            minutes = minutes < 10 ? '0' + minutes : minutes;
            seconds = seconds < 10 ? '0' + seconds : seconds;
            strTime = hours + ':' + minutes + ':' + seconds + ' ' + ampm;
            return date.toDateString() + ' - ' + strTime;
        },
        ConvertBetweenTimeZone: function (time, toTimeZone) {
            if (!toTimeZone)
                return;
            var d = new Date(),
            utc = time.getTime() + (time.getTimezoneOffset() * 60000),
            nd = new Date(utc + (3600000 * toTimeZone));
            return DAT.v1.Utils.DateTime.ToAMPM(nd);
        },
        DatePart: {
            GetFullYear: function (date) {
                return date.getFullYear();
            },
            GetMonth: function (date) {
                return date.getMonth();
            },
            GetWeekDay: function (date) {
                return date.getDay();
            },
            GetMonthDay: function (date) {
                return date.getDate();
            },
            GetMonthDayString: function (date) {
                var day = DAT.v1.Utils.DateTime.DatePart.GetMonthDay(date);
                return day.toString() + (day != 11 && (day / 10).toString().indexOf(".1") != -1 ? "st" : day != 12 && (day / 10).toString().indexOf(".2") != -1 ? "nd" : "th");
            },
            GetWeekDayString: function (date) {
                var weekday = new Array(7);
                weekday[0] = "Sunday";
                weekday[1] = "Monday";
                weekday[2] = "Tuesday";
                weekday[3] = "Wednesday";
                weekday[4] = "Thursday";
                weekday[5] = "Friday";
                weekday[6] = "Saturday";

                return weekday[DAT.v1.Utils.DateTime.DatePart.GetWeekDay(date)];
            },
            GetMonthString: function (date) {
                var month = new Array();
                month[0] = "January";
                month[1] = "February";
                month[2] = "March";
                month[3] = "April";
                month[4] = "May";
                month[5] = "June";
                month[6] = "July";
                month[7] = "August";
                month[8] = "September";
                month[9] = "October";
                month[10] = "November";
                month[11] = "December";
                return month[DAT.v1.Utils.DateTime.DatePart.GetMonth(date)];
            },
        }
    },
    Text: {
        CleanPhoneNumber: function (number) {
            if (!number) return number;
            while (number.indexOf("(") != -1) number = number.replace("(", "");
            while (number.indexOf(")") != -1) number = number.replace(")", "");
            while (number.indexOf("-") != -1) number = number.replace("-", "");
            while (number.indexOf(" ") != -1) number = number.replace(" ", "");
            return number;
        },
        Remove: function (text, start_index, count) {
            return (text.slice(0, start_index) + text.slice(start_index + count));
        },
        InsertAt: function (text, text_to_insert, index) {
            return (text.slice(0, index) + text_to_insert + text.slice(index + Math.abs(0)));
        },
        Replace: function (text, text_to_be_replaced, text_to_replace) {
            while (text.indexOf(text_to_be_replaced) != -1)
                text = text.replace(text_to_be_replaced, text_to_replace);
            return text;
        }
    },
    Image: {
        Thubmnail: function (file, callback) {
            var reader = new FileReader();
            reader.onload = (function (theFile) {
                return function (e) {
                    callback(jQuery("<img>").attr("src", e.target.result));
                };
            })(file);
            reader.readAsDataURL(file);
        },
    },
    Threading: {
        Thread: function (options) {
            var me = this;
            var onReady = null;
            var func = function () {
                if (me.Pause)
                    return;
                me.Occurance++;
                me.OnThick(me, onReady);
            };
            onReady = function () {
                window.setTimeout(func, me.Interval);
                return true;
            };
            this.Start = function () {
                me.Pause = false;
                func();
            };
            this.Occurance = 0;
            this.OnThick = options.OnThick;
            this.Interval = options.Interval;
            this.Pause = true;
            this.Resume = function () {
                me.Pause = false;
                func();
            };
            if (!DAT.v1.Utils.Threading.Current)
                DAT.v1.Utils.Threading.Current = new DAT.v1.Utils.Threading.ThreadPool();
            DAT.v1.Utils.Threading.Current.Threads.push(me);
        },
        ThreadPool: function () {
            var me = this;
            this.Threads = new Array();
            this.RuningThreads = function () {
                var runing_threads = new Array();
                for (var i = 0 ; i < me.Threads.length; i++) {
                    var thread = me.Threads[i];
                    if (!thread.Pause)
                        runing_threads.push(thread);
                }
                return runing_threads;
            };
        }
    },
    Cookies: {
        Get: function (n) {
            var c_value = document.cookie;
            var c_start = c_value.indexOf(" " + n + "=");
            if (c_start == -1) {
                c_start = c_value.indexOf(n + "=");
            }
            if (c_start == -1) {
                c_value = null;
            }
            else {
                c_start = c_value.indexOf("=", c_start) + 1;
                var c_end = c_value.indexOf(";", c_start);
                if (c_end == -1) {
                    c_end = c_value.length;
                }
                c_value = unescape(c_value.substring(c_start, c_end));
            }
            return c_value;
        },
        Set: function (n, v, e) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + e);
            var c_value = escape(v) + ((e == null) ? "" : "; expires=" + exdate.toUTCString());
            document.cookie = n + "=" + c_value;
        }
    },
    Element: {
        GetUniqueID: function (prefix) {
            var generate_new_one = function () {
                return prefix + "_" + Math.floor((Math.random() * 10000000) + 1);
            }
            var random_id = generate_new_one();
            while (document.getElementById(prefix + "_" + Math.floor((Math.random() * 10000000) + 1))) random = generate_new_one();
            return random_id;
        },
        GetCaretPosition: function (element) {
            // Initialize
            var iCaretPos = 0;
            // IE Support
            if (document.selection) {
                // Set focus on the element
                element.focus();

                // To get cursor position, get empty selection range
                var oSel = document.selection.createRange();

                // Move selection start to 0 position
                oSel.moveStart('character', -element.value.length);

                // The caret position is selection length
                iCaretPos = oSel.text.length;
            }
                // Firefox support
            else if (element.selectionStart || element.selectionStart == '0')
                iCaretPos = element.selectionStart;

            // Return results
            return (iCaretPos);
        },
        GetElementValue: function ($element) {
            switch ($element.prop("tagName")) {
                case "INPUT": {
                    switch ($element.attr("type")) {
                        case "radio":
                        case "checkbox": {
                            return $element.is(":checked");
                            break;
                        };
                        case "text": {
                            return $element.val();
                        };
                    };
                    break;
                };
                case "SOURCE": {
                    return $element.attr("src");
                };
                case "TEXTAREA":
                case "SELECT": {
                    return $element.val();
                };
                default:
                    $element.text();
                    break;
            }
        },
        SetElementValue: function ($element, value) {
            switch ($element.prop("tagName")) {
                case "INPUT": {
                    switch ($element.attr("type")) {
                        case "radio":
                        case "checkbox": {
                            $element.prop('checked', value);
                            break;
                        };
                        case "text": {
                            return $element.val(value);
                        }
                    }
                    break;
                };
                case "SOURCE": {
                    return $element.attr("src", value);
                    break;
                };
                case "TEXTAREA":
                case "SELECT": {
                    return $element.val(value);
                }
                default:
                    $element.text(value);
                    break;
            }
        }
    },
    Enumerations: {
        HttpVerbs: {
            POST: "POST",
            GET: "GET",
            PUT: "PUT",
            DELETE: "DELETE",
            HEAD: "HEAD"
        },
        ReponseTypes: {
            OK: {
                Name: "ok",
                Code: 200,
                Description: ""
            },
            Unexpected: {
                Name: "unexpected_error",
                Code: 111,
                Description: ""
            },
            Busy: {
                Name: "busy",
                Code: 100,
                Description: ""
            },
            Failed: {
                Name: "failed",
                Code: 222,
                Description: ""
            }
        }
    },
    Ajax: {
        Call: function (options) {
            var hasContent = options.Method != DAT.v1.Utils.Enumerations.HttpVerbs.GET;

            if (options.Parameters)
                for (var i = 0 ; i < options.Parameters.length ; i++)
                    options.Url += (i == 0 ? "/" : "") + options.Parameters[i] + (i == (options.Parameters.length - 1) ? "" : "/");

            if (options.QueryStrings)
                for (var i = 0 ; i < options.QueryStrings.length ; i++) {
                    var query = options.QueryStrings[i];
                    options.Url += (i == 0 ? "?" : "") + query.Key + "=" + (query.Value === undefined ? "" : query.Value) + (i == (options.QueryStrings.length - 1) ? "" : "&");
                }

            return jQuery.ajax({
                headers: { "Http-Auth": options.AuthenticationToken ? options.AuthenticationToken : "", "Language": options.Language ? options.Language : DAT.v1.Config.Language ? DAT.v1.Config.Language : "en-US" },
                type: options.Method,
                url: options.Url,
                data: hasContent && options.Data ? JSON.stringify(options.Data, null, 2) : "",
                contentType: options.ContentType,
                dataType: options.Accepts,
                success: function (response) {
                    response = typeof (response) == "string" ? JSON.parse(response) : response;
                    if (response.status.code == DAT.v1.Utils.Enumerations.ReponseTypes.OK.Code || response.status.code == DAT.v1.Utils.Enumerations.ReponseTypes.Busy.Code)
                        options.OnSuccess(response);
                    else if (options.OnError)
                        options.OnError("OnError", response);
                },
                fail: function (e) {
                    if (options.OnError)
                        options.OnError("OnFail", e);
                },
                abort: function (e) {
                    if (options.OnError)
                        options.OnError("OnAbort", e);
                },
                error: function (e) {
                    if (options.OnError)
                        options.OnError("OnUnexpectedError", e);
                }
            });
        }
    }
};