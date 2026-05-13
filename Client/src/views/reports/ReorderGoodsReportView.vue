<script setup lang="ts">
import ReportTable from '@/components/ReportTable.vue'
import { reorderGoodsReportConfig } from '@/consts/tableConfigs'
</script>

<template>
  <Card>
    <template #content>
      <div class="entity-title">
        <h1>Отчёт по товарам, которые скоро закончатся</h1>
        <Button label="На домашнюю" as="router-link" :to="{ name: 'Home' }" />
      </div>
      <Divider></Divider>
      <ReportTable
        api-service="Reports"
        report="GetReorderGoods"
        :config="reorderGoodsReportConfig"
      ></ReportTable>
    </template>
  </Card>
</template>
