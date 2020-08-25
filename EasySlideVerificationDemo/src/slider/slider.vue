<template>
    <div class="slider-panel" :style="{width:bgWidth+'px'}">
        <img ref="backgroudImg" :src="backgroundImg" />
        <img ref="slideImg" class="cover" :style="slideImgStyle" :src="slideImg" />
        <div class="slider" pre="slider_path"></div>
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
            bgWidth: Number,
            slideWidth: Number,
            backgroundImg: String,
            slideImg: String,
            positionX: Number,
            positionY: Number
        },
        data() {
            return {
                hovering: false,
                dragging: false,
                startX: 0,
                currentX: 0,
                startY: 0,
                currentY: 0,
                startPosition: 0,
                currentPosition: 0
            }
        },
        computed: {
            slideImgStyle() {
                var left = `${this.currentPosition / this.bgWidth * 100}%`;
                var top = `${this.positionY}px`;
                return { top: top, left: left };
            },
            sliderBtnStyle() {
                var left = `${this.currentPosition / this.bgWidth * 100}%`;
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
                this.startX = event.clientX;
                this.startPosition = this.currentPosition;
            },

            onDragging(event) {
                if (this.dragging) {
                    if (event.type === 'touchmove') {
                        event.clientY = event.touches[0].clientY;
                        event.clientX = event.touches[0].clientX;
                    }
                    this.currentX = event.clientX;
                    this.currentPosition = this.startPosition + (this.currentX - this.startX);
                    if (this.currentPosition > this.bgWidth - this.slideWidth) {
                        this.currentPosition = this.bgWidth - this.slideWidth;
                    } else if (this.currentPosition < 0) {
                        this.currentPosition = 0;
                    }
                }
            },

            onDragEnd() {
                if (this.dragging) {
                    this.dragging = false;
                }
            }
        }
    };
</script>

<style type="text/css">
    .slider-panel {
        margin: 0 auto;
        position: relative;
        width: 100%;
    }

    .slider {
        background-color: #e4e7ed;
        padding: 4px 0;
        margin: 10px 0;
        width: 100%;
        height: 0px;
    }

    .slider-button {
        position: absolute;
        display: block;
        text-align:center;
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
</style>
