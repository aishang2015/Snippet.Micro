

<!--模板-->
<template>
  <div class="full-window">
    <div>登录</div>
    <div v-if="this.isLogin">登录了</div>
    <div v-if="!this.isLogin">未登录</div>
  </div>
</template>

<!--样式-->
<style scoped lang="less">
.full-window {
  width: 100%;
  height: 100%;
  background: lightgray;
}
</style>

<!--逻辑-->
<script lang="ts">
import { Vue } from "vue-class-component";

export default class LoginView extends Vue {
  
  // data可以直接声明为类属性
  private isLogin = true;

  // methods可以直接声明为类方法
  public isGood(): string {
    return "111";
  }

  // 所有Vue生命周期挂钩也可以直接声明为类原型方法，但是您不能在实例本身上调用它们。
  // 声明自定义方法时，应避免使用这些保留名称。
  mounted() {
    this.isLogin =
      localStorage.getItem("token") !== null &&
      localStorage.getItem("token") !== undefined &&
      localStorage.getItem("token") !== "";
  }
}
</script>