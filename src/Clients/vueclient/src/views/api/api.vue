

<!--模板-->
<template>
  <div class="api-container">
      <iframe src="http://192.168.0.148:20000/rbac/swagger/index.html"></iframe>
  </div>
</template>

<!--样式-->
<style scoped lang="less">
.api-container {
  width: 500px;
  height: 500px;
  overflow: auto;
}
</style>

<!--逻辑-->
<script lang="ts">
import { Vue } from "vue-class-component";

export default class ApiView extends Vue {}
</script>