<template>
    <div class="slider-panel" :style="{width:bgWidth+'px'}">
        <img ref="backgroudImg" :src="backgroundImg" />
        <img ref="slideImg" class="cover" :style="slideImgStyle" :src="slideImg" />
        <div class="slider" pre="slider_path" :class="{success:success}"></div>
        <div class="slider-button" :style="sliderBtnStyle" ref="button"
             @mouseenter="handleMouseEnter"
             @mouseleave="handleMouseLeave"
             @mousedown="onButtonDown"
             @touchstart="onButtonDown"
             @mousemove="onDragging"
             @touchmove="onDragging"
             @mouseup="onDragEnd"
             @touchend="onDragEnd">
            <span class="slider-button-inner"></span>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'Slider',
        components: {},
        props: {
            loadDataFunc: { type: Function, default: null },
            verifyFunc: { type: Function, default: null }
        },
        data() {
            return {
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
                positionY: 0,
                success: false
            }
        },
        created() {
            this.loadData();
        },
        computed: {
            slideImgStyle() {
                var left = `${this.positionX / this.bgWidth * 100}%`;
                var top = `${this.positionY}px`;
                return { top: top, left: left };
            },
            sliderBtnStyle() {
                var left = `${this.positionX / this.bgWidth * 100}%`;
                var cursor = "pointer";
                if (this.hovering == true) {
                    cursor = "grab";
                }
                if (this.dragging == true) {
                    cursor = "grabbing";
                }
                return { left: left, cursor: cursor };
            }
        },
        methods: {
            handleMouseEnter() {
                this.hovering = true;
            },

            handleMouseLeave() {
                this.hovering = false;
            },

            onButtonDown(event) {
                event.preventDefault();
                this.onDragStart(event);
            },

            onDragStart(event) {
                this.dragging = true;
                if (event.type === 'touchstart') {
                    event.clientY = event.touches[0].clientY;
                    event.clientX = event.touches[0].clientX;
                }
                this.drageStartX = event.clientX;
                this.prePositionX = this.positionX;
            },

            onDragging(event) {
                if (this.dragging) {
                    if (event.type === 'touchmove') {
                        event.clientY = event.touches[0].clientY;
                        event.clientX = event.touches[0].clientX;
                    }
                    this.positionX = this.prePositionX + (event.clientX - this.drageStartX);
                    if (this.positionX > this.bgWidth - this.slideWidth) {
                        this.positionX = this.bgWidth - this.slideWidth;
                    } else if (this.positionX < 0) {
                        this.positionX = 0;
                    }
                }
            },
            //拖动结束
            onDragEnd() {
                if (this.dragging) {
                    this.dragging = false;
                    this.verify();
                }
            },
            //获取数据
            loadData() {
                if (this.loadDataFunc) {
                    var that = this;
                    this.loadDataFunc(function (data) {
                        that.key = data.key;
                        that.bgWidth = data.bgWidth;
                        that.slideWidth = data.slideWidth;
                        that.backgroundImg = data.backgroundImg;
                        that.slideImg = data.slideImg;
                        that.positionX = data.positionX;
                        that.positionY = data.positionY;
                    });
                }
            },
            //校验结果
            verify() {
                if (this.verifyFunc) {
                    var that = this;
                    var data = { key: this.key, positionX: this.positionX, positionY: this.positionY }
                    this.verifyFunc(data, function (success) {
                        that.success = success;
                    });
                }
            }
        }
    };
</script>

<style>
    .slider-panel {
        position: relative;
        margin: 0 auto;
        width: 100%;
    }

    .slider {
        background-color: #e4e7ed;
        padding: 4px 0;
        margin: 10px 0;
        height: 0;
        width: 100%;
    }

    .slider-button {
        position: absolute;
        display: block;
        text-align: center;
        width: 40px;
        height: 20px;
        user-select: none;
        cursor: pointer;
        margin-top: -26px;
    }

    .slider-button-inner {
        display: inline-block;
        width: 20px;
        height: 20px;
        border: 2px solid #409eff;
        background-color: #fff;
        border-radius: 50%;
    }

    .cover {
        position: absolute;
    }

    .success {
        animation: slider 0.6s;
        -webkit-animation: slider 0.6s;
        background-color: #42b983;
    }

    @keyframes slider {
        from {
            background-color: #e4e7ed;
        }

        to {
            background-color: #42b983;
        }
    }

    @-webkit-keyframes myfirst /* Safari 与 Chrome */
    {
        from {
            background-color: #e4e7ed;
        }

        to {
            background-color: #42b983;
        }
    }
</style>
