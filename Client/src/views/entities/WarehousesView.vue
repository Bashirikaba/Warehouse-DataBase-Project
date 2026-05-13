<script setup lang="ts">
import { warehousesConfig } from '@/consts/tableConfigs'
</script>

<template>
  <Card>
    <template #content>
      <div class="entity-title">
        <h1>Склады</h1>
        <Button label="На домашнюю" as="router-link" :to="{ name: 'Home' }" />
      </div>
      <Divider></Divider>
      <EntityTable api-service="Warehouses" :config="warehousesConfig"></EntityTable>
    </template>
  </Card>
</template>
