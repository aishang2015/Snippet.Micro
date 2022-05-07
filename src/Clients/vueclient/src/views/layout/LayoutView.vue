

<!--模板-->
<template>
  <div id="layout-container">
    <el-menu mode="horizontal" id="nav-header">
      <span id="title">snippet-micro微服务中心</span>
      <div id="action-bar"></div>
    </el-menu>
    <div id="layout-body">
      <el-menu id="nav-sider" default-active="2">
        <router-link to="/" class="no-decroation">
          <el-menu-item index="1">
            <el-icon><money /></el-icon>
            <span>仪表盘</span>
          </el-menu-item>
        </router-link>
        <el-sub-menu index="2">
          <template #title>
            <el-icon><postcard /></el-icon>
            <span>权限管理</span>
          </template>
          <router-link to="/about" class="no-decroation">
            <el-menu-item index="2-1">用户管理</el-menu-item>
          </router-link>
          <el-menu-item index="2-2">角色管理</el-menu-item>
          <el-menu-item index="2-3">组织管理</el-menu-item>
          <el-menu-item index="2-4">权限管理</el-menu-item>
        </el-sub-menu>

        <el-sub-menu index="3">
          <template #title>
            <el-icon><operation /></el-icon>
            <span>运维管理</span>
          </template>
          <el-menu-item index="3-1">网关管理</el-menu-item>
          <el-menu-item index="3-2">服务管理</el-menu-item>
          <router-link to="/api" class="no-decroation">
            <el-menu-item index="3-3">接口管理</el-menu-item>
          </router-link>
        </el-sub-menu>
      </el-menu>
      <div id="view-body">
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>

<!--样式-->
<style scoped lang="less">
@screen-height: 100vh;
@screen-width: 100vw;

@sider-width: 240px;
@body-margin: 10px;

#layout-container {
  width: @screen-width;
  height: @screen-height;
  display: flex;
  flex-direction: column;

  #nav-header {
    position: fixed;
    width: @screen-width;
    user-select: none;
    border-bottom-color: lightblue;
    z-index: 100;
    justify-content: space-between;

    #title {
      line-height: 58px;
      font-size: 22px;
      margin: 0 30px;
      color: #409eff;
    }

    #action-bar {
      width: 100px;
      background: rebeccapurple;
      height: 58px;
      align-self: flex-end;
    }
  }

  #layout-body {
    margin-top: 58px;
    width: @screen-width;
    height: 100%;
    background: lightgray;

    // 边栏
    #nav-sider {
      user-select: none;
      position: fixed;
      width: @sider-width;
      height: calc(@screen-width - 60px);

      .no-decroation {
        text-decoration: none;
      }
    }

    // 视图显示内容
    #view-body {
      width: calc(@screen-width - @sider-width - 2 * @body-margin);
      height: calc(@screen-height - 60px - 2 * @body-margin);
      margin: @body-margin @body-margin @body-margin
        calc(@sider-width + @body-margin);
      background: white;
      overflow: auto;
    }
  }
}
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