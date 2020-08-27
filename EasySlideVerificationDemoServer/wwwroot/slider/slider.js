var template = '\
<div class="slider-panel">\
    <img id="backgroundImg" />\
    <img id="slideImg" class="cover" />\
    <div id="slidePath" class="slider" pre="slider_path" ></div>\
    <div id="slideBtn" class="slider-button" >\
        <span class="slider-button-inner"></span>\
    </div>\
</div>';
var Slider = function (container, loadDataFunc, verifyFunc) {
    this.container = container;
    this.container.append(template);
    this.loadDataFunc = loadDataFunc;
    this.verifyFunc = verifyFunc;

    this.backgroundImg = null;
    this.slideImg = null;
    this.sliderBtn = null;
    this.data = {
        hovering: false,
        dragging: false,
        drageStartX: 0,
        drageStartY: 0,
        prePositionX: 0,
        //
        key: '',
        bgWidth: 0,
        slideWidth: 0,
        backgroundImg: '',
        slideImg: '',
        positionX: 0,
        positionY: 0
    };
    this._init();
}
Slider.prototype = {
    _init() {
        this.backgroundImg = $("#backgroundImg", this.container);
        this.slideImg = $("#slideImg", this.container);
        this.sliderBtn = $("#slideBtn", this.container);
        var that = this;
        this.sliderBtn.on("mouseenter", function () { that.data.hovering = true; })
            .on("mouseleave", function () { that.data.hovering = false; })
            .on("mousedown", function (event) { that.onButtonDown(event) })
            .on("touchstart", function (event) { that.onButtonDown(event) })
            .on("mousemove", function (event) { that.onDragging(event) })
            .on("touchmove", function (event) { that.onDragging(event) })
            .on("mouseup", function (event) { that.onDragEnd(event) })
            .on("touchend", function (event) { that.onDragEnd(event) });

        this.loadData();
    },

    onButtonDown(event) {
        event.preventDefault();
        this.onDragStart(event);
    },

    onDragStart(event) {
        this.data.dragging = true;
        if (event.type === 'touchstart') {
            event.clientY = event.touches[0].clientY;
            event.clientX = event.touches[0].clientX;
        }
        this.data.drageStartX = event.clientX;
        this.data.prePositionX = this.data.positionX;
    },

    onDragging(event) {
        if (this.data.dragging) {
            if (event.type === 'touchmove') {
                event.clientY = event.touches[0].clientY;
                event.clientX = event.touches[0].clientX;
            }
            this.data.positionX = this.data.prePositionX + (event.clientX - this.data.drageStartX);
            if (this.data.positionX > this.data.bgWidth - this.data.slideWidth) {
                this.data.positionX = this.data.bgWidth - this.data.slideWidth;
            } else if (this.data.positionX < 0) {
                this.data.positionX = 0;
            }
            this.setSlideImgStyle();
            this.setSliderBtnStyle();
        }
    },
    //拖动结束
    onDragEnd(event) {
        if (this.data.dragging) {
            this.data.dragging = false;
            this.verify();
        }
    },
    setSlideImgStyle() {
        var left = `${this.data.positionX / this.data.bgWidth * 100}%`;
        var top = `${this.data.positionY}px`;
        this.slideImg.css({ top: top, left: left });
    },
    setSliderBtnStyle() {
        var left = `${this.data.positionX / this.data.bgWidth * 100}%`;
        var cursor = "pointer";
        if (this.data.hovering == true) {
            cursor = "grab";
        }
        if (this.data.dragging == true) {
            cursor = "grabbing";
        }
        this.sliderBtn.css({ left: left, cursor: cursor });
    },
    //获取数据
    loadData() {
        if (this.loadDataFunc) {
            var that = this;
            this.loadDataFunc(function (data) {
                that.loadDataCallback(data);
            });
        }
    },
    loadDataCallback(data) {
        this.data.key = data.key;
        this.data.bgWidth = data.bgWidth;
        this.data.slideWidth = data.slideWidth;
        this.data.backgroundImg = data.backgroundImg;
        this.data.slideImg = data.slideImg;
        this.data.positionX = data.positionX;
        this.data.positionY = data.positionY;

        $(".slider-panel", this.constructor).css({ width: data.bgWidth });
        this.backgroundImg.attr("src", data.backgroundImg);
        this.slideImg.attr("src", data.slideImg);
        this.setSlideImgStyle();
        this.setSliderBtnStyle();
    },
    //校验结果
    verify() {
        if (this.verifyFunc) {
            var that = this;
            var data = { key: this.data.key, positionX: this.data.positionX, positionY: this.data.positionY }
            this.verifyFunc(data, function (success) {
                if (success) {
                    $("#slidePath", that.container).addClass("success");
                }
            });
        }
    }
}

