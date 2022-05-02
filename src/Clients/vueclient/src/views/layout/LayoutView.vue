

<!--模板-->
<template>
  <div>布局页</div>
  <el-button @click="logout">登出</el-button>
  <el-button type="primary">Primary</el-button>
  <router-view></router-view>
</template>

<!--样式-->
<style scoped lang="less">
</style>

<!--逻辑-->
<script lang="ts">
import { Vue } from "vue-class-component";

export default class LayoutView extends Vue {
  logout() {
    localStorage.removeItem("token");
    location.reload();
  }
}
</script>