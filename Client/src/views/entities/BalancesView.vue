<script setup lang="ts">
import { balancesConfig } from '@/consts/tableConfigs'
</script>

<template>
  <Card>
    <template #content>
      <div class="entity-title">
        <h1>Баланс товаров на складах</h1>
        <Button label="На домашнюю" as="router-link" :to="{ name: 'Home' }" />
      </div>
      <Divider></Divider>
      <EntityTable api-service="Balances" :config="balancesConfig"></EntityTable>
    </template>
  </Card>
</template>
