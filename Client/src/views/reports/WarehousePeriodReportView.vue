<script setup lang="ts">
import ReportTable from '@/components/ReportTable.vue'
import { warehousePeriodReportConfig } from '@/consts/tableConfigs'
</script>

<template>
  <Card>
    <template #content>
      <div class="entity-title">
        <h1>Отчёт по складам за период</h1>
        <Button label="На домашнюю" as="router-link" :to="{ name: 'Home' }" />
      </div>
      <Divider></Divider>
      <ReportTable
        api-service="Reports"
        report="GetWarehousePeriod"
        :config="warehousePeriodReportConfig"
      ></ReportTable>
    </template>
  </Card>
</template>
